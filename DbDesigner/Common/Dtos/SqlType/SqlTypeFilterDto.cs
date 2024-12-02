namespace Common.Dtos.SqlType;

public class SqlTypeFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public bool? HasParams { get; set; }
    
    public List<int> DataBases { get; set; } = [];
    
    public List<int> LanguageTypes { get; set; } = [];
}