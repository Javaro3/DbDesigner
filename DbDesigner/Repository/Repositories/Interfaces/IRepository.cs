using Common.Domain;
using Common.Domain.BaseDomain;

namespace Repository.Repositories.Interfaces;

public interface IRepository<T> where T : BaseModel 
{
    public IQueryable<T> Query();
    
    public Task<T> GetAsync(int id);
    
    public Task<IEnumerable<T>> GetAsync();
    
    public Task<bool> DeleteAsync(int id);
    
    public Task UpdateAsync(T entity);
    
    public Task AddAsync(T entity);
}