using Common.Domain;
using Common.Dtos.LanguageType;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class LanguageTypeHelper : IBaseHelper<LanguageType, LanguageTypeFilterDto>
{
    public IQueryable<LanguageType> ApplySort(IQueryable<LanguageType> query, LanguageTypeFilterDto filter)
    {
        return query;
    }

    public IQueryable<LanguageType> ApplyFilter(IQueryable<LanguageType> query, LanguageTypeFilterDto filter)
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
            query = query.Where(x => filter.Languages.All(e => e == x.Language!.Id));    
        }
        
        if (filter.SqlTypes.Any())
        {
            query = query.Where(x => filter.SqlTypes.All(e => x.SqlTypes.Select(d => d.Id).Contains(e)));
        }
        
        return query;
    }
}