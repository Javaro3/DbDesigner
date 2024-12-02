using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly Expression<Func<User, User>> _selector = u => new User
    {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email,
        CreatedOn = u.CreatedOn,
        Roles = u.Roles.Select(e => new Role
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description
        }).ToList(),
        Projects = u.Projects.Select(e => new Project
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            CreatedOn = e.CreatedOn
        }).ToList()
    };
    
    public UserRepository(DbDesignerContext context) : base(context)
    {
    }
    
    public override IQueryable<User> GetAll()
    {
        return _dbSet.Include(e => e.Roles)
            .Include(e => e.Projects)
            .Select(_selector)
            .AsQueryable();
    }
    
    public override async Task<User?> GetByIdAsync(int id)
    {
        return await _dbSet.Include(e => e.Roles)
            .Include(e => e.Projects)
            .Select(_selector)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> IsNewAsync(string email)
    {
        return _dbSet.AllAsync(e => e.Email != email);
    }

    public Task<User?> GetUserByEmail(string email)
    {
        return _dbSet.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Email == email);
    }

    public override async Task AddAsync(User entity)
    {
        var roles = entity.Roles.ToList();
        entity.Roles.Clear();
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        foreach (var role in roles)
        {
            var userRole = new UserRole { UserId = entity.Id, RoleId = role.Id };
            await _context.AddAsync(userRole);
        }
        await _context.SaveChangesAsync();
    }

    public override async Task UpdateAsync(User entity)
    {
        var userRoles = _context.UserRoles.Where(e => e.UserId == entity.Id).ToList();
        _context.RemoveRange(userRoles);
        
        var roles = entity.Roles.ToList();
        entity.Roles.Clear();
        _dbSet.Update(entity);
        
        foreach (var role in roles)
        {
            var userRole = new UserRole { UserId = entity.Id, RoleId = role.Id };
            await _context.AddAsync(userRole);
        }
        await _context.SaveChangesAsync();
    }
}