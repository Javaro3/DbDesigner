using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.LanguageType;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class LanguageTypeDataService : BaseDataService<LanguageType, LanguageTypeDto, LanguageTypeFilterDto, ComboboxDto>
{
    public LanguageTypeDataService(
        IRepository<LanguageType> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<LanguageType, LanguageTypeFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}