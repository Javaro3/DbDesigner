using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Table;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class TableController(IBaseDataService<Table, TableDto, FilterRequestDto, ComboboxDto> dataService)
    : BaseController<Table, TableDto, FilterRequestDto, ComboboxDto>(dataService);