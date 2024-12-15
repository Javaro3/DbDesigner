namespace Common.GenerateModels;

public class DataTableGenerateModel
{
    public int TableId { get; set; }

    public string TableName { get; set; } = string.Empty;

    public List<DataFieldGenerateModel> Fields { get; set; } = [];
    
    public int RowCount { get; set; }
}