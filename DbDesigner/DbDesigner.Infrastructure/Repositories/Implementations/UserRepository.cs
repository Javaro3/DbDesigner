using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class UserRepository(DbDesignerContext context)
    : Repository<User>(context), IUserRepository
{
    public override IQueryable<User> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<User?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> IsNewAsync(string email)
    {
        return DbSet.AllAsync(e => e.Email != email);
    }

    public Task<User?> GetUserByEmail(string email)
    {
        return IncludeExpression(DbSet).FirstOrDefaultAsync(e => e.Email == email);
    }

    public override async Task<User> AddAsync(User entity)
    {
        var roles = entity.Roles.ToList();
        entity.Roles.Clear();
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();

        foreach (var role in roles)
        {
            var userRole = new UserRole { UserId = entity.Id, RoleId = role.Id };
            await Context.AddAsync(userRole);
        }
        await Context.SaveChangesAsync();
        return entity;
    }

    public override async Task<User> UpdateAsync(User entity)
    {
        var userRoles = Context.UserRoles.Where(e => e.UserId == entity.Id).ToList();
        Context.RemoveRange(userRoles);
        
        var roles = entity.Roles.ToList();
        entity.Roles.Clear();
        DbSet.Update(entity);
        
        foreach (var role in roles)
        {
            var userRole = new UserRole { UserId = entity.Id, RoleId = role.Id };
            await Context.AddAsync(userRole);
        }
        await Context.SaveChangesAsync();
        return entity;
    }

    private static IIncludableQueryable<User, ICollection<Role>> IncludeExpression(IQueryable<User> query)
    {
        return query
            .Include(e => e.Roles);
    }
}