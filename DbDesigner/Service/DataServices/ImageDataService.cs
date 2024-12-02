using Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Interfaces.Infrastructure.DataServices;
using ImageDto = Common.Dtos.ImageDto;

namespace Service.DataServices;

public class ImageDataService : IImageDataService
{
    private readonly string _path;

    public ImageDataService(IOptions<ImageOptions> options)
    {
        _path = options.Value.Path;
            
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }
    
    public async Task<string> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new Exception("File is empty.");
        }
        
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(_path, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }

    public ImageDto GetImage(string fileName)
    {
        var filePath = Path.Combine(_path, fileName);

        if (!File.Exists(filePath))
            throw new Exception("Image not found.");

        var imageBytes = File.ReadAllBytes(filePath);
        var contentType = "image/" + Path.GetExtension(fileName).TrimStart('.');
            
        return new ImageDto { ImageBytes = imageBytes, ContentType = contentType };
    }

    public void DeleteImage(string fileName)
    {
        var filePath = Path.Combine(_path, fileName);

        if (!File.Exists(filePath))
            throw new Exception("Image not found.");

        File.Delete(filePath);
    }
}