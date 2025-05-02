using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.RelationAction;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;


[Authorize(Roles = "User")]
public class RelationActionController(IBaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto> dataService)
    : BaseController<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto>(dataService);