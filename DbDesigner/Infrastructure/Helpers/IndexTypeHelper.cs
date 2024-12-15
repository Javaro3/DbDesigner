using Common.Domain;
using Common.Dtos.IndexType;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class IndexTypeHelper : IBaseHelper<IndexType, IndexTypeFilterDto>
{
    public IQueryable<IndexType> ApplySort(IQueryable<IndexType> query, IndexTypeFilterDto filter)
    {
        return query;
    }

    public IQueryable<IndexType> ApplyFilter(IQueryable<IndexType> query, IndexTypeFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }

        if (filter.DataBases.Any())
        {
            query = query.Where(x => filter.DataBases.All(e => x.DataBases.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}