using Common.Dtos.Column;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IColumnDataService
{
    Task<ColumnDiagramDto> AddColumnToTableAsync(ColumnBaseDto column, int tableId);
    
    Task DeleteAsync(int id);
    
    Task UpdateAsync(ColumnBaseDto column);
}