using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Property;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class PropertyDataService : BaseDataService<Property, PropertyDto, PropertyFilterDto, HasParamsComboboxDto>, IPropertyDataService
{
    public PropertyDataService(
        IRepository<Property> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Property, PropertyFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
    
    public List<HasParamsComboboxDto> GetForComboboxByDataBase(int dataBaseId)
    {
        var query = Repository.Get().Where(e => e.DataBaseId == dataBaseId);
        var dtos = Mapper.Map<List<HasParamsComboboxDto>>(query);
        return dtos;
    }
}