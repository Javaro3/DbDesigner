using DbDesigner.Application.Dtos;
using Microsoft.AspNetCore.Http;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IImageDataService
{
   Task<string> UploadImage(IFormFile file);
    
    ImageDto GetImage(string fileName);

    void DeleteImage(string fileName);
}