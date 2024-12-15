using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Language;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

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