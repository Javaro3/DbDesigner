using Common.Dtos.Orm;

namespace Common.Dtos.Language;

public class LanguageDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public string Image { get; set; } = string.Empty;
    
    public List<OrmDto> Orms { get; set; } = [];
}