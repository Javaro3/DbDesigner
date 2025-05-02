using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.IndexType;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class IndexTypeController(IIndexTypeDataService dataService)
    : BaseController<IndexType, IndexTypeDto, IndexTypeFilterDto, ComboboxDto>(dataService)
{
    [HttpGet("get-combobox-by-database")]
    public IResult GetForCombobox(int dataBaseId)
    {
        var data = dataService.GetForComboboxByDataBase(dataBaseId);
        return Results.Json(data);
    }
}