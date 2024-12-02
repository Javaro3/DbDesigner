using Common.Dtos.ColumnProperty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class ColumnPropertyController
{
    private readonly IColumnPropertyDataService _columnPropertyDataService;

    public ColumnPropertyController(IColumnPropertyDataService columnPropertyDataService)
    {
        _columnPropertyDataService = columnPropertyDataService;
    }

    [HttpPost("add-to-column")]
    public async Task<IResult> AddToColumn(ColumnPropertyAddDto dto)
    {
        await _columnPropertyDataService.AddPropertyToColumnAsync(dto.ColumnProperty!, dto.ColumnId);
        return Results.Ok();
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] ColumnPropertyUpdateDto dto)
    {
        try
        {
            await _columnPropertyDataService.UpdateAsync(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IResult> Delete([FromBody] ColumnPropertyDeleteDto dto)
    {
        try
        {
            await _columnPropertyDataService.DeleteAsync(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}