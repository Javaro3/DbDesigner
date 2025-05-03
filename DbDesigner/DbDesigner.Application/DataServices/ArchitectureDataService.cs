using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Architecture;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class ArchitectureDataService : BaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto>
{
    public ArchitectureDataService(
        IRepository<Architecture> repository,
        IDataSourceHelper dataSourceHelper, 
        IBaseHelper<Architecture,ArchitectureFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}