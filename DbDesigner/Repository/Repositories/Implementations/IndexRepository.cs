using System.Linq.Expressions;
using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Index = Common.Domain.Index;

namespace Repository.Repositories.Implementations;

public class IndexRepository : Repository<Index>, IIndexRepository
{
    private readonly Expression<Func<Index, Index>> _forDiagramSelector = index => new Index
    {
        Id = index.Id,
        Description = index.Description,
        IndexTypeId = index.IndexTypeId,
        Columns = index.Columns.Select(column => new Column 
            {
                Id = column.Id
            })
            .ToList()
    };
    
    public IndexRepository(DbDesignerContext context) : base(context)
    {
    }

    public async Task<List<Index>> GetForDiagramAsync(IEnumerable<int> columnIds)
    {
        return await _dbSet.Where(e => e.Columns.Any(i => columnIds.Contains(i.Id))).Select(_forDiagramSelector).ToListAsync();
    }

    public override async Task AddAsync(Index entity)
    {
        var columnIds = entity.Columns.Select(e => e.Id).ToList();
        entity.Columns.Clear();

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        foreach (var columnId in columnIds)
        {
            var indexColumn = new IndexColumn
            {
                IndexId = entity.Id,
                ColumnId = columnId
            };
            await _context.AddAsync(indexColumn);
        }
        
        await _context.SaveChangesAsync();

        var entityId = entity.Id;
        entity = (await _dbSet.Include(e => e.Columns).FirstOrDefaultAsync(e => e.Id == entityId))!;
    }
    
    public override async Task UpdateAsync(Index entity)
    {
        var columnEntityIds = entity.Columns.Select(e => e.Id).ToList();
        entity.Columns.Clear();

        var indexColumns = _context.IndexColumns.Where(e => e.IndexId == entity.Id).ToList();
        _context.IndexColumns.RemoveRange(indexColumns);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        foreach (var columnId in columnEntityIds)
        {
            var indexColumn = new IndexColumn
            {
                IndexId = entity.Id,
                ColumnId = columnId
            };
            await _context.AddAsync(indexColumn);
        }
        
        await _context.SaveChangesAsync();
    }
}