using Common.Dtos.Language;
using Common.Dtos.SqlType;

namespace Common.Dtos.LanguageType;

public class LanguageTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public LanguageDto? Language { get; set; }

    public List<SqlTypeDto> SqlTypes { get; set; } = [];
}