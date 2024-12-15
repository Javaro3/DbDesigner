using Common.Dtos.SqlType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class SqlTypeController : Controller
{
    private readonly ISqlTypeDataService _sqlTypeDataService;

    public SqlTypeController(ISqlTypeDataService sqlTypeDataService)
    {
        _sqlTypeDataService = sqlTypeDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] SqlTypeFilterDto filter)
    {
        var data = await _sqlTypeDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _sqlTypeDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _sqlTypeDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox-by-database")]
    public IResult GetForCombobox(int dataBaseId)
    {
        var data = _sqlTypeDataService.GetForComboboxByDataBase(dataBaseId);
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] SqlTypeDto dto)
    {
        try
        {
            await _sqlTypeDataService.UpdateAsync(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IResult> Delete([FromQuery] int id)
    {
        try
        {
            await _sqlTypeDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}