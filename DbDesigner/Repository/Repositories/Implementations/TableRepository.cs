using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class TableRepository : Repository<Table>, ITableRepository
{
    private readonly IColumnRepository _columnRepository;
    
    public TableRepository(DbDesignerContext context, IColumnRepository columnRepository) : base(context)
    {
        _columnRepository = columnRepository;
    }

    public async Task AddTableToProjectAsync(Table table, int projectId)
    {
        await AddAsync(table);

        var projectTable = new ProjectTable
        {
            ProjectId = projectId,
            TableId = table.Id
        };

        await _context.AddAsync(projectTable);
        await _context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(Table entity)
    {
        var columns = (await _dbSet.Include(e => e.Columns).FirstOrDefaultAsync(e => e.Id == entity.Id))!.Columns.ToList();

        foreach (var column in columns)
            await _columnRepository.DeleteAsync(column);

        await base.DeleteAsync(entity);
    }
}