namespace Common.GenerateModels;

public class DataFieldGenerateModel
{
    public string FieldName { get; set; } = string.Empty;

    public string FieldType { get; set; } = string.Empty; 
    
    public string? FieldRequires { get; set; }
    
    public int? RelationTableId { get; set; }
}