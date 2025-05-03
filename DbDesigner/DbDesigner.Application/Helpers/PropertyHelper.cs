using DbDesigner.Application.Dtos.Property;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

public class PropertyHelper : IBaseHelper<Property, PropertyFilterDto>
{
    public IQueryable<Property> ApplySort(IQueryable<Property> query, PropertyFilterDto filter)
    {
        return query;
    }

    public IQueryable<Property> ApplyFilter(IQueryable<Property> query, PropertyFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.HasParams != null)
        {
            query = query.Where(x => filter.HasParams == x.HasParams);    
        }
        
        if (filter.DataBases.Any())
        {
            query = query.Where(x => filter.DataBases.Contains(x.DataBaseId));    
        }
        
        return query;
    }
}