using Index = Common.Domain.Index;

namespace Repository.Repositories.Interfaces;

public interface IIndexRepository : IRepository<Index>
{
    Task<List<Index>> GetForDiagramAsync(IEnumerable<int> columnIds);
}