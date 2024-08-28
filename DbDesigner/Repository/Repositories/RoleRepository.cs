using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class RoleRepository : IRepository<Role>
{
    private readonly DbDesignerContext _context;

    public RoleRepository(DbDesignerContext context)
    {
        _context = context;
    }
    
    public IQueryable<Role> Query()
    {
        return _context.Roles.AsQueryable();
    }

    public async Task<Role> GetAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<IEnumerable<Role>> GetAsync()
    {
        return await _context.Roles
            .Include(e => e.Users)
            .Include(e => e.Permissions)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Roles.FindAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;    
    }

    public async Task UpdateAsync(Role entity)
    {
        _context.Roles.Update(entity);
        await _context.SaveChangesAsync();    
    }

    public async Task AddAsync(Role entity)
    {
        await _context.Roles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}