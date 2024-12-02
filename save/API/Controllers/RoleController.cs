using Common.Domain;
using Common.Dtos;
using Common.Dtos.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class RoleController : Controller
{
    private readonly IBaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto> _roleDataService;

    public RoleController(IBaseDataService<Role, RoleDto, RoleFilterDto, ComboboxDto> roleDataService)
    {
        _roleDataService = roleDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] RoleFilterDto filter)
    {
        var data = await _roleDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _roleDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _roleDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] RoleDto dto)
    {
        try
        {
            await _roleDataService.UpdateAsync(dto);
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
            await _roleDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}