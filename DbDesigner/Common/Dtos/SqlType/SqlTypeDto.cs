using Common.Dtos.DataBase;
using Common.Dtos.LanguageType;

namespace Common.Dtos.SqlType;

public class SqlTypeDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool HasParams { get; set; }
    
    public ICollection<DataBaseDto> DataBases { get; set; } = [];
    
    public ICollection<LanguageTypeDto> LanguageTypes { get; set; } = [];
}