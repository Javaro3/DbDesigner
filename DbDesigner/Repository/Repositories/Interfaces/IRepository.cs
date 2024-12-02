using Common.Domain.BaseDomain;

namespace Repository.Repositories.Interfaces;

public interface IRepository<T> where T : BaseModel 
{
    IQueryable<T> GetAll();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}