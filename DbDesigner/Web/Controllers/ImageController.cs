using Common.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Controllers;

public class ImageController : Controller
{
    private readonly string _basePath;

    public ImageController(IOptions<ImageSettings> imageSettings)
    {
        _basePath = imageSettings.Value.BasePath;
    }

    public IActionResult GetImage(string filename)
    {
        var path = Path.Combine(_basePath, filename);

        if (!System.IO.File.Exists(path))
        {
            return NotFound();
        }

        var image = System.IO.File.OpenRead(path);
        var extension = Path.GetExtension(filename).ToLowerInvariant();

        var contentType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".svg" => "image/svg+xml",
            ".gif" => "image/gif",
            _ => "application/octet-stream"
        };

        return File(image, contentType);
    }
}