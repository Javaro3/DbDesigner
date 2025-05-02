using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Role;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class RoleController(IBaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto> dataService)
    : BaseController<Role, RoleDto, RoleFilterDto, ComboboxDto>(dataService);