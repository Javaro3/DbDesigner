namespace Common.Dtos.Language;

public class LanguageFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public ICollection<int> Orms { get; set; } = [];
}