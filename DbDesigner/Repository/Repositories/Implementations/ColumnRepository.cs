using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class ColumnRepository : Repository<Column>, IColumnRepository
{
    public ColumnRepository(DbDesignerContext context) : base(context)
    {
    }

    public async Task AddColumnToTableAsync(Column column, int tableId)
    {
        await AddAsync(column);

        var tableColumn = new TableColumn
        {
            TableId = tableId,
            ColumnId = column.Id
        };

        await _context.AddAsync(tableColumn);
        await _context.SaveChangesAsync();
    }
    
    public override async Task DeleteAsync(Column entity)
    {
        var relations = await _context.Relations.Where(e => e.SourceColumnId == entity.Id || e.TargetColumnId == entity.Id).ToListAsync();
        var indexColumns = await _context.IndexColumns.Where(e => e.ColumnId == entity.Id).ToListAsync();
        var indexColumnIds = indexColumns.ConvertAll(e => e.IndexId);
        var indexes = await _context.Indices.Where(e => indexColumnIds.Contains(e.Id)).ToListAsync();
        var columnProperties = await _context.ColumnProperties.Where(e => e.ColumnId == entity.Id).ToListAsync();

        _context.RemoveRange(relations);
        _context.RemoveRange(indexColumns);
        _context.RemoveRange(indexes);
        _context.RemoveRange(columnProperties);
        await _context.SaveChangesAsync();
        
        await base.DeleteAsync(entity);
    }
}