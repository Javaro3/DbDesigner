namespace Common.GenerateModels;

public class TableGenerateModel
{
    public int TableId { get; set; }
    
    public string TableName { get; set; } = string.Empty;

    public IEnumerable<ColumnGenerateModel> Columns { get; set; } = [];
    
    public IEnumerable<RelationGenerateModel> Relations { get; set; } = [];
}