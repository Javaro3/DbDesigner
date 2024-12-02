using Common.Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Implementations;

public class ColumnPropertyRepository : Repository<ColumnProperty>, IColumnPropertyRepository
{
    public ColumnPropertyRepository(DbDesignerContext context) : base(context)
    {
    }

    public async Task<List<ColumnProperty>> GetForDiagramAsync(int columnId)
    {
        return await _dbSet.Where(e => e.ColumnId == columnId).ToListAsync();
    }

    public async Task AddPropertyToColumnAsync(ColumnProperty columnProperty, int columnId)
    {
        var newColumnProperty = new ColumnProperty
        {
            PropertyId = columnProperty.PropertyId,
            ColumnId = columnId,
            PropertyParams = columnProperty.PropertyParams
        };

        await AddAsync(newColumnProperty);
    }

    public async Task UpdatePropertyToColumnAsync(ColumnProperty columnProperty, int prevPropertyId)
    {
        if (columnProperty.PropertyId == prevPropertyId)
        {
            await UpdateAsync(columnProperty);
            return;
        }
        
        var model = await _dbSet.FirstOrDefaultAsync(e => e.PropertyId == prevPropertyId && e.ColumnId == columnProperty.ColumnId);
        await DeleteAsync(model);
        await AddAsync(columnProperty);
    }
}