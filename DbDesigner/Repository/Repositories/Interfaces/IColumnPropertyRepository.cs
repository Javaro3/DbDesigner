using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface IColumnPropertyRepository : IRepository<ColumnProperty>
{
    Task<List<ColumnProperty>> GetForDiagramAsync(int columnId);
    
    Task AddPropertyToColumnAsync(ColumnProperty columnProperty, int columnId);
    
    Task UpdatePropertyToColumnAsync(ColumnProperty columnProperty, int prevPropertyId);
}