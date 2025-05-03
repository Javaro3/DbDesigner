using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class PropertyRepository(DbDesignerContext context)
    : Repository<Property>(context)
{
    public override IQueryable<Property> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<Property?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Property> AddAsync(Property entity)
    {
        entity.DataBase = null;
        await base.AddAsync(entity);
        return entity;
    }

    public override async Task<Property> UpdateAsync(Property entity)
    {
        entity.DataBase = null;
        await base.UpdateAsync(entity);
        return entity;
    }
    
    private static IIncludableQueryable<Property, DataBase?> IncludeExpression(IQueryable<Property> query)
    {
        return query
            .Include(e => e.DataBase);
    }
}