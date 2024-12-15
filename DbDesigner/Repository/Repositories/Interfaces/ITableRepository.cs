using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface ITableRepository : IRepository<Table>
{
    Task AddTableToProjectAsync(Table table, int projectId);
}