using Common.Dtos;
using Microsoft.AspNetCore.Http;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IImageDataService
{
   Task<string> UploadImage(IFormFile file);
    
    ImageDto GetImage(string fileName);

    void DeleteImage(string fileName);
}