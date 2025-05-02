using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class RelationDataService : BaseDataService<Relation, RelationDto, FilterRequestDto, ComboboxDto>
{
    public RelationDataService(
        IRepository<Relation> repository,
        IDataSourceHelper dataSourceHelper,
        IMapper mapper) : base(repository, dataSourceHelper, null, mapper)
    {
    }
}