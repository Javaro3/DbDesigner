namespace Common.Dtos.LanguageType;

public class LanguageTypeFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public List<int> Languages { get; set; } = [];

    public List<int> SqlTypes { get; set; } = [];
}