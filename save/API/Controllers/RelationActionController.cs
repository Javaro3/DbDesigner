using Common.Domain;
using Common.Dtos;
using Common.Dtos.RelationAction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class RelationActionController : Controller
{
    private readonly IBaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto> _relationActionDataService;

    public RelationActionController(IBaseDataService<RelationAction, RelationActionDto, RelationActionFilterDto, ComboboxDto> relationActionDataService)
    {
        _relationActionDataService = relationActionDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] RelationActionFilterDto filter)
    {
        var data = await _relationActionDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _relationActionDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _relationActionDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] RelationActionDto dto)
    {
        try
        {
            await _relationActionDataService.UpdateAsync(dto);
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
            await _relationActionDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}