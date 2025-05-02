using System.Security.Claims;
using DbDesigner.Application.Dtos.User;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DbDesigner.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthDataService _authDataService;
    private readonly FrontendOptions _frontendOptions;

    public AuthController(IAuthDataService authDataService, IOptions<FrontendOptions> frontendOptions)
    {
        _authDataService = authDataService;
        _frontendOptions = frontendOptions.Value;
    }
    
    [HttpPost("login")]
    public async Task<IResult> Login(UserLoginDto dto)
    {
        try
        {
            var token = await _authDataService.LoginAsync(dto);
            return Results.Json(token);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
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
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
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
            return Results.Redirect($"{_frontendOptions.Url}?{token.Token}");
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}