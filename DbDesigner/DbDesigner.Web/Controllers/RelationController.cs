using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class RelationController(IBaseDataService<Relation, RelationDto, FilterRequestDto, ComboboxDto> dataService)
    : BaseController<Relation, RelationDto, FilterRequestDto, ComboboxDto>(dataService);