namespace Common.GenerateModels;

public class ColumnGenerateModel
{
    public int ColumnId { get; set; }

    public string ColumnName { get; set; } = string.Empty;
    
    public SqlTypeGenerateModel? SqlType { get; set; }

    public IEnumerable<PropertyGenerateModel> Properties { get; set; } = [];
}