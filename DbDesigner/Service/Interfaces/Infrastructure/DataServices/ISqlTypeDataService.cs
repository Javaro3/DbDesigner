using Common.Domain;
using Common.Dtos;
using Common.Dtos.SqlType;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface ISqlTypeDataService : IBaseDataService<SqlType, SqlTypeDto, SqlTypeFilterDto, HasParamsComboboxDto>
{
    List<HasParamsComboboxDto> GetForComboboxByDataBase(int dataBaseId);
}