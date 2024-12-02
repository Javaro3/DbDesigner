using Common.Dtos.Relation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class RelationController : Controller
{
    private readonly IRelationDataService _relationDataService;

    public RelationController(IRelationDataService relationDataService)
    {
        _relationDataService = relationDataService;
    }
    
    [HttpPost("add-to-project")]
    public async Task<IResult> AddToProject(RelationDto dto)
    {
        var newRelation = await _relationDataService.AddToProjectAsync(dto);
        return Results.Json(newRelation);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] RelationDto dto)
    {
        try
        {
            await _relationDataService.UpdateAsync(dto);
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
            await _relationDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}