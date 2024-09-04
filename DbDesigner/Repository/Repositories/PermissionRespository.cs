using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public class PermissionRespository : IRepository<Permission>
{
    private readonly DbDesignerContext _context;

    public PermissionRespository(DbDesignerContext context)
    {
        _context = context;
    }
    
    public IQueryable<Permission> Query()
    {
        return _context.Permissions.AsQueryable();
    }

    public async Task<Permission> GetAsync(int id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    public async Task<IEnumerable<Permission>> GetAsync()
    {
        return await _context.Permissions
            .Include(e => e.Roles)
            .ThenInclude(e => e.Users)
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Permissions.FindAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;   
    }

    public async Task UpdateAsync(Permission entity)
    {
        _context.Permissions.Update(entity);
        await _context.SaveChangesAsync();    
    }

    public async Task AddAsync(Permission entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}