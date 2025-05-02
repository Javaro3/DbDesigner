namespace Repository.Repositories.Interfaces;

public interface IRepository<TModel>
    where TModel: class
{
    IQueryable<TModel> Get();

    Task<TModel?> GetAsync(int id);

    Task<TModel> AddAsync(TModel entity);

    Task<TModel> UpdateAsync(TModel entity);

    Task DeleteAsync(TModel entity);
}