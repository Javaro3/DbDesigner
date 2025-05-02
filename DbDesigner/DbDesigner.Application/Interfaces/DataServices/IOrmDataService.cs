using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IOrmDataService : IBaseDataService<Orm, OrmDto, OrmFilterDto, ComboboxDto> 
{
    List<ComboboxDto> GetForComboboxByLanguage(int languageId);
}