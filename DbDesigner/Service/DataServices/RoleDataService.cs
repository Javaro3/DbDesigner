using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Role;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class RoleDataService : BaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto>
{
    public RoleDataService(
        IRepository<Role> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Role, RoleFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}