using Common.Dtos;
using Common.Dtos.Architecture;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;
using Architecture = Common.Domain.Architecture;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class ArchitectureController : Controller
{
    private readonly IBaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto> _architectureDataService;

    public ArchitectureController(IBaseDataService<Architecture, ArchitectureDto, ArchitectureFilterDto, ComboboxDto> architectureDataService)
    {
        _architectureDataService = architectureDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] ArchitectureFilterDto filter)
    {
        var data = await _architectureDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _architectureDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _architectureDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] ArchitectureDto dto)
    {
        try
        {
            await _architectureDataService.UpdateAsync(dto);
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
            await _architectureDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}