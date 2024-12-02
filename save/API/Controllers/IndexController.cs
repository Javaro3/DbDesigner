using Common.Dtos.Index;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class IndexController : Controller
{
    private readonly IIndexDataService _indexDataService;

    public IndexController(IIndexDataService indexDataService)
    {
        _indexDataService = indexDataService;
    }
    
    [HttpPost("add-to-table")]
    public async Task<IResult> AddToTable(IndexDto dto)
    {
        var newIndex = await _indexDataService.AddAsync(dto);
        return Results.Json(newIndex);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] IndexDto dto)
    {
        try
        {
            await _indexDataService.UpdateAsync(dto);
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
            await _indexDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}