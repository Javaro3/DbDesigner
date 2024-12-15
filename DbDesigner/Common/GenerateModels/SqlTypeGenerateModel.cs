namespace Common.GenerateModels;

public class SqlTypeGenerateModel
{
    public int SqlTypeId { get; set; }

    public string SqlTypeName { get; set; } = string.Empty;
    
    public string? Params { get; set; }
}