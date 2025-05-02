using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Column;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class ColumnController(IBaseDataService<Column, ColumnDto, FilterRequestDto, ComboboxDto> dataService)
    : BaseController<Column, ColumnDto, FilterRequestDto, ComboboxDto>(dataService);