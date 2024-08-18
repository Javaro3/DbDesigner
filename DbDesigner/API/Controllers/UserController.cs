using Common.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DataServicies;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("/login")]
    public async Task<IResult> Login(UserLoginDto dto)
    {
        try
        {
            var jws = await _userService.Login(dto);
            HttpContext.Response.Cookies.Append("jwt", jws);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message);
        }
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(UserRegisterDto dto)
    {
        await _userService.Register(dto);
        return Results.Ok();
    }
}