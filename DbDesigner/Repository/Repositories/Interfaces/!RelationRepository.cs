using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface IRelationRepository : IRepository<Relation>
{
    Task<List<Relation>> GetForDiagramAsync(IEnumerable<int> columnIds);
}