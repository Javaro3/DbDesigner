using DbDesigner.Application.Dtos.User;
using DbDesigner.Application.Interfaces.DataServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class UserController : Controller
{
    private readonly IUserDataService _userDataService;

    public UserController(IUserDataService userDataService)
    {
        _userDataService = userDataService;
    }

    [HttpGet("current-user")]
    [Authorize(Roles = "User")]
    public async Task<IResult> GetCurrentUser()
    {
        var user = await _userDataService.GetCurrentUserAsync(Request.Headers.Authorization!);

        if (user == null)
        {
            return Results.Unauthorized();
        }
        
        return Results.Ok(user);
    }
    
    [HttpGet]
    public async Task<IResult> GetAll([FromQuery] UserFilterDto filter)
    {
        var data = await _userDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IResult> Get(int id)
    {
        var data = await _userDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("combobox")]
    public IResult GetForCombobox()
    {
        var data = _userDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost]
    public async Task<IResult> Update([FromBody] UserDto dto)
    {
        try
        {
            await _userDataService.UpdateAsync(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpPost("add")]
    public async Task<IResult> Add([FromBody] UserAddDto dto)
    {
        try
        {
            await _userDataService.AddUserWithRole(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            await _userDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}