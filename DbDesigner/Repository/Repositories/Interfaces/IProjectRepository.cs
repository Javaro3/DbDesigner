using Common.Domain;

namespace Repository.Repositories.Interfaces;

public interface IProjectRepository : IRepository<Project>
{ 
    Task<Project?> GetForDiagramByIdAsync(int id);
}