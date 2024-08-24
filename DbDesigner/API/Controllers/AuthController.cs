using System.Security.Claims;
using Common.Dtos.UserDtos;
using Common.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Service.DataServicies;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("/login")]
    public async Task<IResult> Login(UserLoginDto dto)
    {
        try
        {
            var jwt = await _authService.Login(dto);
            HttpContext.Response.Cookies.Append("jwt", jwt);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(UserRegisterDto dto)
    {
        try
        {
            await _authService.Register(dto);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }

    [HttpGet("/google-login")]
    public IResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Results.Challenge(properties, [AuthScheme.Google.ToString()]);
    }

    [HttpGet("/google-response")]
    public async Task<IResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result.Principal == null)
            return Results.BadRequest();
        
        var name = result.Principal.FindFirst(ClaimTypes.Name)!.Value;
        var email = result.Principal.FindFirst(ClaimTypes.Email)!.Value;
        
        try
        {
            var jwt = await _authService.GoogleLogin(name, email);
            HttpContext.Response.Cookies.Append("jwt", jwt);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    
    [HttpPost("/logout")]
    public async Task<IResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Response.Cookies.Delete("jwt");
        return Results.Ok("Successfully logged out");
    }
}