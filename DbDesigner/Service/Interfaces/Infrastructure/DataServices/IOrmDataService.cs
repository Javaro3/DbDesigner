using Common.Domain;
using Common.Dtos;
using Common.Dtos.Orm;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IOrmDataService : IBaseDataService<Orm, OrmDto, OrmFilterDto, ComboboxDto> 
{
    List<ComboboxDto> GetForComboboxByLanguage(int languageId);
}