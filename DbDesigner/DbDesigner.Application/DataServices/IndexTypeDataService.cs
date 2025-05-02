using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.IndexType;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class IndexTypeDataService : BaseDataService<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>, IIndexTypeDataService
{
    public IndexTypeDataService(
        IRepository<IndexType> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<IndexType, IndexTypeFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }

    public List<ComboboxDto> GetForComboboxByDataBase(int dataBaseId)
    {
        var query = Repository.Get().Where(e => e.DataBaseId == dataBaseId);
        var dtos = Mapper.Map<List<ComboboxDto>>(query);
        return dtos;
    }
}