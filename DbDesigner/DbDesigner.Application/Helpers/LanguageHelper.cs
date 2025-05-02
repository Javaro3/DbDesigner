using DbDesigner.Application.Dtos.Language;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

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
        
        return query;
    }
}