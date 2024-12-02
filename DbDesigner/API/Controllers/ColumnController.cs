using Common.Dtos.Column;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class ColumnController
{
    private readonly IColumnDataService _columnDataService;

    public ColumnController(IColumnDataService columnDataService)
    {
        _columnDataService = columnDataService;
    }

    [HttpPost("add-to-table")]
    public async Task<IResult> AddToTable(ColumnAddDto dto)
    {
        var newColumn = await _columnDataService.AddColumnToTableAsync(dto.Column!, dto.TableId);
        return Results.Json(newColumn);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] ColumnBaseDto dto)
    {
        try
        {
            await _columnDataService.UpdateAsync(dto);
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
            await _columnDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}