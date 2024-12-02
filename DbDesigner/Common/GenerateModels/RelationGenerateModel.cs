namespace Common.GenerateModels;

public class RelationGenerateModel
{
    public int TargetColumnId { get; set; }
    public string TargetColumnName { get; set; } = string.Empty;
    
    public int TargetTableId { get; set; }
    public string TargetTableName { get; set; } = string.Empty;
    
    public int SourceColumnId { get; set; }
    public string SourceColumnName { get; set; } = string.Empty;
    
    public int SourceTableId { get; set; }
    public string SourceTableName { get; set; } = string.Empty;

    public int OnDelete { get; set; }

    public int OnUpdate { get; set; }

    public string RelationName => $"FK_{Guid.NewGuid().ToString()[..8]}";
}