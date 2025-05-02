using DbDesigner.Application.Dtos.SqlType;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

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
            query = query.Where(x => filter.DataBases.Contains(x.DataBaseId));    
        }
        
        return query;
    }
}