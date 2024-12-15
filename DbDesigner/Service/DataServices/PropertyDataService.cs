using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Property;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class PropertyDataService : BaseDataService<Property, PropertyDto, PropertyFilterDto, HasParamsComboboxDto>
{
    public PropertyDataService(
        IRepository<Property> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Property, PropertyFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}