namespace Common.GenerateModels;

public class IndexGenerateModel
{
    public int IndexType { get; set; }

    public string TableName { get; set; } = string.Empty;

    public IEnumerable<string> ColumnNames { get; set; } = [];

    public string IndexName => $"IDX_{Guid.NewGuid().ToString()[..8]}";
}