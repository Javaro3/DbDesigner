using Common.Domain;
using Common.Dtos.Architecture;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class ArchitectureHelper : IBaseHelper<Architecture, ArchitectureFilterDto>
{
    public IQueryable<Architecture> ApplySort(IQueryable<Architecture> query, ArchitectureFilterDto filter)
    {
        return query;
    }

    public IQueryable<Architecture> ApplyFilter(IQueryable<Architecture> query, ArchitectureFilterDto filter)
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