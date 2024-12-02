using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.IndexType;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

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
        var query = _repository.GetAll().Where(e => e.DataBases.Select(d => d.Id).Contains(dataBaseId));
        var dtos = _mapper.Map<List<ComboboxDto>>(query);
        return dtos;
    }
}