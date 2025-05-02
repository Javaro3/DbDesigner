using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class RelationRepository(DbDesignerContext context)
    : Repository<Relation>(context)
{
    public override IQueryable<Relation> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<Relation?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }
    
    private static IIncludableQueryable<Relation, RelationAction?> IncludeExpression(IQueryable<Relation> query)
    {
        return query
            .Include(e => e.SourceColumn)
            .Include(e => e.TargetColumn)
            .Include(e => e.OnDelete)
            .Include(e => e.OnUpdate);
    }
}