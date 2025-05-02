using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Language;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class LanguageDataService : BaseDataService<Language, LanguageDto, LanguageFilterDto, ComboboxDto>
{
    public LanguageDataService(
        IRepository<Language> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Language,LanguageFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}