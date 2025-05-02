using System.Linq.Expressions;
using DbDesigner.Domain.Domain;
using DbDesigner.Domain.Domain.BaseDomain;
using Microsoft.EntityFrameworkCore.Query;

namespace DbDesigner.Infrastructure.Repositories.Interfaces;

public interface IRepository<TModel>
    where TModel : BaseModel 
{
    IQueryable<TModel> Get();

    Task<TModel?> GetAsync(int id);

    Task<TModel> AddAsync(TModel entity);

    Task<TModel> UpdateAsync(TModel entity);

    Task DeleteAsync(TModel entity);
}