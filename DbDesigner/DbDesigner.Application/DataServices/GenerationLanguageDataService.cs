using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationLanguage;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class GenerationLanguageDataService : BaseDataService<GenerationLanguage, GenerationLanguageDto, GenerationLanguageFilterDto, ComboboxDto>
{
    public GenerationLanguageDataService(
        IRepository<GenerationLanguage> repository,
        IDataSourceHelper dataSourceHelper, 
        IBaseHelper<GenerationLanguage, GenerationLanguageFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}