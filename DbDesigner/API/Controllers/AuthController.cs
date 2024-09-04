using System.Security.Claims;
using Common.Constants;
using Common.Dtos.UserDtos;
using Common.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
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
            var jwt = await _authService.LoginAsync(dto);
            HttpContext.Response.Cookies.Append("jwt", jwt);
            return Results.Ok();
        }
        catch (ValidationException e)
        {
            return Results.BadRequest(e.ValidationResult);
        }
    }

    [HttpPost("/register")]
    public async Task<IResult> Register(UserRegisterDto dto)
    {
        try
        {
            await _authService.RegisterAsync(dto);
            return Results.Ok();
        }
        catch (ValidationException e)
        {
            return Results.BadRequest(e.ValidationResult);
        }
    }

    [HttpGet("/google-login")]
    public IResult GoogleLogin()
    {
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Results.Challenge(properties, [GoogleDefaults.AuthenticationScheme]);
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
            var jwt = await _authService.GoogleLoginAsync(name, email);
            HttpContext.Response.Cookies.Append("jwt", jwt);
            return Results.Ok();
        }
        catch (ValidationException e)
        {
            return Results.BadRequest(e.ValidationResult);
        }
    }
    
    [HttpPost("/logout")]
    [Authorize(Policy.Auth.Logout)]
    public async Task<IResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Response.Cookies.Delete("jwt");
        return Results.Ok("Successfully logged out");
    }
}