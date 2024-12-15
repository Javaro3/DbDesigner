using Common.Domain;
using Common.Dtos.Language;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class LanguageHelper : IBaseHelper<Language, LanguageFilterDto>
{
    public IQueryable<Language> ApplySort(IQueryable<Language> query, LanguageFilterDto filter)
    {
        return query;
    }

    public IQueryable<Language> ApplyFilter(IQueryable<Language> query, LanguageFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (filter.Orms.Any())
        {
            query = query.Where(x => filter.Orms.All(e => x.Orms.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}