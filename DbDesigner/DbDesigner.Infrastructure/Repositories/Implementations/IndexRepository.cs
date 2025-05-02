using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class IndexRepository(DbDesignerContext context)
    : Repository<Index>(context)
{
    public override IQueryable<Index> Get()
    {
        return IncludeExpression(DbSet);
    }

    public override async Task<Index?> GetAsync(int id)
    {
        return await Get().FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<Index> AddAsync(Index entity)
    {
        var columnIds = entity.Columns.Select(e => e.Id).ToList();
        entity.Columns.Clear();

        await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();

        foreach (var columnId in columnIds)
        {
            var indexColumn = new IndexColumn
            {
                IndexId = entity.Id,
                ColumnId = columnId
            };
            await Context.AddAsync(indexColumn);
        }
        
        await Context.SaveChangesAsync();

        return (await GetAsync(entity.Id))!;
    }
    
    public override async Task<Index> UpdateAsync(Index entity)
    {
        var columnEntityIds = entity.Columns.Select(e => e.Id).ToList();
        entity.Columns.Clear();

        var indexColumns = await Context.IndexColumns.Where(e => e.IndexId == entity.Id).ToListAsync();
        Context.IndexColumns.RemoveRange(indexColumns);
        DbSet.Update(entity);
        await Context.SaveChangesAsync();

        foreach (var columnId in columnEntityIds)
        {
            var indexColumn = new IndexColumn
            {
                IndexId = entity.Id,
                ColumnId = columnId
            };
            await Context.AddAsync(indexColumn);
        }
        
        await Context.SaveChangesAsync();
        return (await GetAsync(entity.Id))!;
    }
    
    private static IIncludableQueryable<Index, IndexType?> IncludeExpression(IQueryable<Index> query)
    {
        return query
            .Include(e => e.Columns)
                .ThenInclude(e => e.Table)
            .Include(e => e.IndexType);
    }
}