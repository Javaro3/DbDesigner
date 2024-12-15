using System.Linq.Expressions;
using Common.Domain;
using Common.Dtos.Relation;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class RelationRepository : Repository<Relation>, IRelationRepository
{
    private readonly Expression<Func<Relation, Relation>> _forDiagramSelector = relation => new Relation
    {
        Id = relation.Id,
        OnDeleteId = relation.OnDeleteId,
        OnUpdateId = relation.OnUpdateId,
        SourceColumnId = relation.SourceColumnId,
        SourceColumn = relation.SourceColumn != null
            ? new Column
            {
                Id = relation.SourceColumn.Id,
                Name = relation.SourceColumn.Name,
                Description = relation.SourceColumn.Description
            }
            : null,
        TargetColumnId = relation.TargetColumnId,
        TargetColumn = relation.TargetColumn != null
            ? new Column
            {
                Id = relation.TargetColumn.Id,
                Name = relation.TargetColumn.Name,
                Description = relation.TargetColumn.Description
            }
            : null,
    };
    
    public RelationRepository(DbDesignerContext context) : base(context)
    {
    }

    public async Task<List<Relation>> GetForDiagramAsync(IEnumerable<int> columnIds)
    {
        columnIds = columnIds.ToList();
        return await _dbSet.Where(e => columnIds.Contains(e.SourceColumnId) || columnIds.Contains(e.TargetColumnId)).Select(_forDiagramSelector).ToListAsync();
    }
}