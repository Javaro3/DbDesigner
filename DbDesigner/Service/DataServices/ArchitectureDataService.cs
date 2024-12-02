using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Architecture;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

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