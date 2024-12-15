using Common.Dtos.Table;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface ITableDataService
{
    Task<TableDiagramDto> AddTableToProjectAsync(TableBaseDto table, int projectId);
    
    Task DeleteAsync(int id);
    
    Task UpdateAsync(TableBaseDto table);
}