using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class OrmRepository(DbDesignerContext context)
    : Repository<Orm>(context)
{
    public override IQueryable<Orm> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<Orm?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Orm> AddAsync(Orm entity)
    {
        entity.Language = null;
        await base.AddAsync(entity);
        return entity;
    }

    public override async Task<Orm> UpdateAsync(Orm entity)
    {
        entity.Language = null;
        await base.UpdateAsync(entity);
        return entity;
    }
    
    private static IIncludableQueryable<Orm, Language?> IncludeExpression(IQueryable<Orm> query)
    {
        return query.Include(e => e.Language);
    }
}