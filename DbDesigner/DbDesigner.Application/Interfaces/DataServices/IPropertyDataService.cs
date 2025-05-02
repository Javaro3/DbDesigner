using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Property;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IPropertyDataService : IBaseDataService<Property, PropertyDto, PropertyFilterDto, HasParamsComboboxDto>
{
    List<HasParamsComboboxDto> GetForComboboxByDataBase(int dataBaseId);
}