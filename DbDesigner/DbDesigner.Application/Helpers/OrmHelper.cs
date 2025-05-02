using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

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
            query = query.Where(x => filter.Languages.Contains(x.LanguageId));    
        }
        
        return query;
    }
}