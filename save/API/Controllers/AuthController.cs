using System.Security.Claims;
using Common.Dtos.User;
using Common.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthDataService _authDataService;

    public AuthController(IAuthDataService authDataService)
    {
        _authDataService = authDataService;
    }
    
    [HttpPost("login")]
    public async Task<IResult> Login(UserLoginDto dto)
    {
        try
        {
            var token = await _authDataService.LoginAsync(dto);
            return Results.Json(token);
        }
        catch (ExceptionModel e)
        {
            return Results.BadRequest(e.Model);
        }
    }

    [HttpPost("register")]
    public async Task<IResult> Register(UserRegisterDto dto)
    {
        try
        {
            await _authDataService.RegisterAsync(dto);
            return Results.Ok("You have successfully registered!");
        }
        catch (ExceptionModel e)
        {
            return Results.BadRequest(e.Model);
        }
    }

    [HttpGet("google-login")]
    public IResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Results.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);
    }

    [HttpGet("google-response")]
    public async Task<IResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (result.Principal == null)
            return Results.BadRequest();
        
        var name = result.Principal.FindFirst(ClaimTypes.Name)!.Value;
        var email = result.Principal.FindFirst(ClaimTypes.Email)!.Value;
        
        try
        {
            var token = await _authDataService.GoogleLoginAsync(name, email);
            return Results.Json(token);
        }
        catch (ExceptionModel e)
        {
            return Results.BadRequest(e.Model);
        }
    }
}