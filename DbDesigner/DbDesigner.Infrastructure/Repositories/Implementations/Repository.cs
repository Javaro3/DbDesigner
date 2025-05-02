using System.Linq.Expressions;
using DbDesigner.Domain.Domain.BaseDomain;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class Repository<TModel> : IRepository<TModel>
    where TModel: BaseModel
{
    protected readonly DbDesignerContext Context;
    protected readonly DbSet<TModel> DbSet;
    
    public Repository(DbDesignerContext context)
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