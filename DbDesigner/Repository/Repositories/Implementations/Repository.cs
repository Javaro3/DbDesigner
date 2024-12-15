using Common.Domain.BaseDomain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    protected readonly DbDesignerContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(DbDesignerContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}