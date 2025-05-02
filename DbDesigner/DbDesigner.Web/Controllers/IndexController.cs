using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Index;
using DbDesigner.Application.Interfaces.DataServices;
using Microsoft.AspNetCore.Authorization;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class IndexController(IBaseDataService<Index, IndexDto, FilterRequestDto, ComboboxDto> dataService)
    : BaseController<Index, IndexDto, FilterRequestDto, ComboboxDto>(dataService);