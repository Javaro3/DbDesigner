using DbDesigner.Application.Dtos.GenerationModel;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

public class GenerationModelHelper : IBaseHelper<GenerationModel, GenerationModelFilterDto>
{
    public IQueryable<GenerationModel> ApplySort(IQueryable<GenerationModel> query, GenerationModelFilterDto filter)
    {
        return query;
    }

    public IQueryable<GenerationModel> ApplyFilter(IQueryable<GenerationModel> query, GenerationModelFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        return query;
    }
}