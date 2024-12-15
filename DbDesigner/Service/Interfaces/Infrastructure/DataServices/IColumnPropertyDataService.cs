using Common.Dtos.ColumnProperty;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IColumnPropertyDataService
{
    Task AddPropertyToColumnAsync(ColumnPropertyDto columnProperty, int columnId);
    
    Task DeleteAsync(ColumnPropertyDeleteDto columnProperty);
    
    Task UpdateAsync(ColumnPropertyUpdateDto columnProperty);
}