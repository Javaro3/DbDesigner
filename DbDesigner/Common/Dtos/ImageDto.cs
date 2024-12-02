using Microsoft.VisualBasic;

namespace Common.Dtos;

public class ImageDto
{
    public byte[] ImageBytes { get; set; } = [];
    
    public string ContentType { get; set; } = string.Empty;
}