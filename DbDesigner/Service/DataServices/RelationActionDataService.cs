using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.RelationAction;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class RelationActionDataService : BaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto>
{
    public RelationActionDataService(
        IRepository<RelationAction> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<RelationAction, RelationActionFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}