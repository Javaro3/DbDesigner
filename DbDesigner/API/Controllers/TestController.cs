using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : Controller
{
    [HttpGet("/get")]
    public IResult GetData()
    {
        var data = User.Claims.Select(e => e.Value).ToList();
        return Results.Json(data);
    }
}