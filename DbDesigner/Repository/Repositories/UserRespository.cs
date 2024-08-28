using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class UserRespository : IRepository<User>
{
    private readonly DbDesignerContext _context;

    public UserRespository(DbDesignerContext context)
    {
        _context = context;
    }
    
    public IQueryable<User> Query()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<User> GetAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        return await _context.Users
            .Include(e => e.Roles)
            .ThenInclude(e => e.Permissions)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}