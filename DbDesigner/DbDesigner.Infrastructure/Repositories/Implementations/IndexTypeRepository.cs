using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class IndexTypeRepository(DbDesignerContext context)
    : Repository<IndexType>(context)
{
    public override IQueryable<IndexType> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<IndexType?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<IndexType> AddAsync(IndexType entity)
    {
        entity.DataBase = null;
        await base.AddAsync(entity);
        return entity;
    }
    
    public override async Task<IndexType> UpdateAsync(IndexType entity)
    {
        entity.DataBase = null;
        await base.UpdateAsync(entity);
        return entity;
    }

    private static IIncludableQueryable<IndexType, DataBase?> IncludeExpression(IQueryable<IndexType> query)
    {
        return query.Include(e => e.DataBase);
    }
}