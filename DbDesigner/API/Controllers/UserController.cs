using Common.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
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
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] UserFilterDto filter)
    {
        var data = await _userDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _userDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _userDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox-without-current")]
    public async Task<IResult> GetForComboboxWithoutCurrent()
    {
        var currentUserId = (await _userDataService.GetCurrentUserAsync(Request.Headers.Authorization!))!.Id;
        var data = _userDataService.GetForCombobox(currentUserId);
        return Results.Json(data);
    }
    
    [HttpPost("update")]
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
    
    [HttpDelete("delete")]
    public async Task<IResult> Delete([FromQuery] int id)
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