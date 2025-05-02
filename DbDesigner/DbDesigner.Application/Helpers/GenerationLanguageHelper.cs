using DbDesigner.Application.Dtos.GenerationLanguage;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

public class GenerationLanguageHelper : IBaseHelper<GenerationLanguage, GenerationLanguageFilterDto>
{
    public IQueryable<GenerationLanguage> ApplySort(IQueryable<GenerationLanguage> query, GenerationLanguageFilterDto filter)
    {
        return query;
    }

    public IQueryable<GenerationLanguage> ApplyFilter(IQueryable<GenerationLanguage> query, GenerationLanguageFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        return query;
    }
}