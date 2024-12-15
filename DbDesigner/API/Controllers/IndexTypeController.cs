using Common.Domain;
using Common.Dtos;
using Common.Dtos.IndexType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class IndexTypeController : Controller
{
    private readonly IIndexTypeDataService _indexTypeDataService;

    public IndexTypeController(IIndexTypeDataService indexTypeDataService)
    {
        _indexTypeDataService = indexTypeDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] IndexTypeFilterDto filter)
    {
        var data = await _indexTypeDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _indexTypeDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _indexTypeDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox-by-database")]
    public IResult GetForCombobox(int dataBaseId)
    {
        var data = _indexTypeDataService.GetForComboboxByDataBase(dataBaseId);
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] IndexTypeDto dto)
    {
        try
        {
            await _indexTypeDataService.UpdateAsync(dto);
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
            await _indexTypeDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}