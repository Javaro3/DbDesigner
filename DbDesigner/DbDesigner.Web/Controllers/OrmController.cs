using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class OrmController(IOrmDataService dataService)
    : BaseController<Orm, OrmDto, OrmFilterDto, ComboboxDto>(dataService)
{
    [HttpGet("get-combobox-by-language")]
    public IResult GetForComboboxByLanguage(int languageId)
    {
        var data = dataService.GetForComboboxByLanguage(languageId);
        return Results.Json(data);
    }
}