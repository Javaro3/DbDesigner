using Common.Domain;
using Common.Dtos.SqlType;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class SqlTypeHelper : IBaseHelper<SqlType, SqlTypeFilterDto>
{
    public IQueryable<SqlType> ApplySort(IQueryable<SqlType> query, SqlTypeFilterDto filter)
    {
        return query;
    }

    public IQueryable<SqlType> ApplyFilter(IQueryable<SqlType> query, SqlTypeFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name)) 
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (filter.HasParams.HasValue)
        {
            query = query.Where(x => x.HasParams == filter.HasParams);
        }
        
        if (filter.DataBases.Any())
        {
            query = query.Where(x => filter.DataBases.All(e => x.DataBases.Select(d => d.Id).Contains(e)));    
        }
        
        if (filter.LanguageTypes.Any())
        {
            query = query.Where(x => filter.LanguageTypes.All(e => x.LanguageTypes.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}