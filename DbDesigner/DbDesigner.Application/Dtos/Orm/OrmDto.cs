using DbDesigner.Application.Dtos.Language;

namespace DbDesigner.Application.Dtos.Orm;

public class OrmDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public LanguageDto? Language { get; set; }
}