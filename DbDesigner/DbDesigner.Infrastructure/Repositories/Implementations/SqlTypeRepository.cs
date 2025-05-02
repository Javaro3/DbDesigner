using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class SqlTypeRepository(DbDesignerContext context)
    : Repository<SqlType>(context)
{
    public override IQueryable<SqlType> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<SqlType?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<SqlType> AddAsync(SqlType entity)
    {
        entity.DataBase = null;
        await base.AddAsync(entity);
        return entity;
    }

    public override async Task<SqlType> UpdateAsync(SqlType entity)
    {
        entity.DataBase = null;
        await base.UpdateAsync(entity);
        return entity;
    }

    private static IIncludableQueryable<SqlType, DataBase?> IncludeExpression(IQueryable<SqlType> query)
    {
        return query
            .Include(e => e.DataBase);
    }
}