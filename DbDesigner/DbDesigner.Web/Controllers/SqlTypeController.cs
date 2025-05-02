using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.SqlType;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class SqlTypeController(ISqlTypeDataService dataService)
    : BaseController<SqlType, SqlTypeDto, SqlTypeFilterDto, HasParamsComboboxDto>(dataService)
{
    [HttpGet("get-combobox-by-database")]
    public IResult GetForCombobox(int dataBaseId)
    {
        var data = dataService.GetForComboboxByDataBase(dataBaseId);
        return Results.Json(data);
    }
}