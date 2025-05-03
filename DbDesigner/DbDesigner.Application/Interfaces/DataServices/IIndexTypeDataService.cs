using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.IndexType;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IIndexTypeDataService : IBaseDataService<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>
{
    List<ComboboxDto> GetForComboboxByDataBase(int dataBaseId);
}