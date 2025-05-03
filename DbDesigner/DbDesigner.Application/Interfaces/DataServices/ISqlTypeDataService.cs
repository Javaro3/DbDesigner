using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.SqlType;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface ISqlTypeDataService : IBaseDataService<SqlType, SqlTypeDto, SqlTypeFilterDto, HasParamsComboboxDto>
{
    List<HasParamsComboboxDto> GetForComboboxByDataBase(int dataBaseId);
}