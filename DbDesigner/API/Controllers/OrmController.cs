using Common.Domain;
using Common.Dtos;
using Common.Dtos.Orm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class OrmController : Controller
{
    private readonly IOrmDataService _ormDataService;

    public OrmController(IOrmDataService ormDataService)
    {
        _ormDataService = ormDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] OrmFilterDto filter)
    {
        var data = await _ormDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _ormDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _ormDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox-by-language")]
    public IResult GetForComboboxByLanguage(int languageId)
    {
        var data = _ormDataService.GetForComboboxByLanguage(languageId);
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] OrmDto dto)
    {
        try
        {
            await _ormDataService.UpdateAsync(dto);
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
            await _ormDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}