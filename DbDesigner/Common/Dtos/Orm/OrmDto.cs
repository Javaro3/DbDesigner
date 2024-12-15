using Common.Dtos.Language;

namespace Common.Dtos.Orm;

public class OrmDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public ICollection<LanguageDto> Languages { get; set; } = [];
}