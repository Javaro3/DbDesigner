using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Role;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

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