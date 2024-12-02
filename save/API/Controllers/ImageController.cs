using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : Controller
{
    private readonly IImageDataService _imageDataService;
    

    public ImageController(IImageDataService imageDataService)
    {
        _imageDataService = imageDataService;
    }

    [HttpPost("upload")]
    public async Task<IResult> UploadImage(IFormFile file)
    {
        try
        {
            await _imageDataService.UploadImage(file);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpGet("{fileName}")]
    public IResult GetImage(string fileName)
    {
        try
        {
            var file = _imageDataService.GetImage(fileName);
            return Results.File(file.ImageBytes, file.ContentType);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [HttpDelete("{fileName}")]
    public IResult DeleteImage(string fileName)
    {
        try
        {
            _imageDataService.DeleteImage(fileName);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}