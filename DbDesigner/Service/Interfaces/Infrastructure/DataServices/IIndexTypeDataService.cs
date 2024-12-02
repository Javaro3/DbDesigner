using Common.Domain;
using Common.Dtos;
using Common.Dtos.IndexType;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IIndexTypeDataService : IBaseDataService<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>
{
    List<ComboboxDto> GetForComboboxByDataBase(int dataBaseId);
}