using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Architecture;
using DbDesigner.Application.Interfaces.DataServices;
using Microsoft.AspNetCore.Authorization;
using Architecture = DbDesigner.Domain.Domain.Architecture;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class ArchitectureController(IBaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto> dataService)
    : BaseController<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto>(dataService);