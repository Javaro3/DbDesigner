namespace DbDesigner.Application.Dtos.Language;

public class LanguageFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
}