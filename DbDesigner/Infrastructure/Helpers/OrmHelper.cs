using Common.Domain;
using Common.Dtos.Orm;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class OrmHelper : IBaseHelper<Orm, OrmFilterDto>
{
    public IQueryable<Orm> ApplySort(IQueryable<Orm> query, OrmFilterDto filter)
    {
        return query;
    }

    public IQueryable<Orm> ApplyFilter(IQueryable<Orm> query, OrmFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (filter.Languages.Any())
        {
            query = query.Where(x => filter.Languages.All(e => x.Languages.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}