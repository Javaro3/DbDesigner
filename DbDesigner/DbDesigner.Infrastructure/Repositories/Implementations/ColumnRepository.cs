using DbDesigner.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class ColumnRepository(DbDesignerContext context)
    : Repository<Column>(context)
{
    public override async Task DeleteAsync(Column entity)
    {
        var relations = await Context.Relations.Where(e => e.SourceColumnId == entity.Id || e.TargetColumnId == entity.Id).ToListAsync();
        var indexColumns = await Context.IndexColumns.Where(e => e.ColumnId == entity.Id).ToListAsync();
        var indexColumnIds = indexColumns.ConvertAll(e => e.IndexId);
        var indexes = await Context.Indices.Where(e => indexColumnIds.Contains(e.Id)).ToListAsync();
        var columnProperties = await Context.ColumnProperties.Where(e => e.ColumnId == entity.Id).ToListAsync();
    
        Context.RemoveRange(relations);
        Context.RemoveRange(indexColumns);
        Context.RemoveRange(indexes);
        Context.RemoveRange(columnProperties);
        await Context.SaveChangesAsync();
        
        await base.DeleteAsync(entity);
    }
}