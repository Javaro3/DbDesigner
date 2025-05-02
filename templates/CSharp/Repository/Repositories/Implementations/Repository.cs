using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class Repository<TModel> : IRepository<TModel>
    where TModel: class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TModel> DbSet;
    
    public Repository(DbContext context)
    {
        Context = context;
        DbSet = Context.Set<TModel>();
    }

    public virtual IQueryable<TModel> Get()
    {
        return DbSet;
    }

    public virtual async Task<TModel?> GetAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<TModel> AddAsync(TModel entity)
    {
        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TModel> UpdateAsync(TModel entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(TModel entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
}