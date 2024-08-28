using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : Controller
{
    [HttpGet("/get")]
    [Authorize(Policy = PolicyName.TestGet)]
    public IResult GetData()
    {
        List<string> data = [
            "123", 
            "1243",
            "12431",
            "12432",
            "12433",
            "12434",
            "12435",
            "12436"
        ];
        return Results.Json(data);
    }
}