using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.ColumnProperty;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class ColumnPropertyController(IBaseDataService<ColumnProperty, ColumnPropertyDto, FilterRequestDto, ComboboxDto> dataService)
    : BaseController<ColumnProperty, ColumnPropertyDto, FilterRequestDto, ComboboxDto>(dataService);