using Common.Dtos.Table;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class TableController : Controller
{
    private readonly ITableDataService _tableDataService;

    public TableController(ITableDataService tableDataService)
    {
        _tableDataService = tableDataService;
    }

    [HttpPost("add-to-project")]
    public async Task<IResult> AddToProject(TableAddDto dto)
    {
        var newTable = await _tableDataService.AddTableToProjectAsync(dto.Table!, dto.ProjectId);
        return Results.Json(newTable);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] TableBaseDto dto)
    {
        try
        {
            await _tableDataService.UpdateAsync(dto);
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
            await _tableDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}