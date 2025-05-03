using DbDesigner.Application.Dtos.RelationAction;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

public class RelationActionHelper : IBaseHelper<RelationAction, RelationActionFilterDto>
{
    public IQueryable<RelationAction> ApplySort(IQueryable<RelationAction> query, RelationActionFilterDto filter)
    {
        return query;
    }

    public IQueryable<RelationAction> ApplyFilter(IQueryable<RelationAction> query, RelationActionFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        return query;
    }
}