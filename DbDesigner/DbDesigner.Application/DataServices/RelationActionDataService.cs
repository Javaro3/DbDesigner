using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.RelationAction;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

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