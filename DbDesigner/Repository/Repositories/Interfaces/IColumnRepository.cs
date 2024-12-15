using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface IColumnRepository : IRepository<Column>
{
    Task AddColumnToTableAsync(Column column, int tableId);
}