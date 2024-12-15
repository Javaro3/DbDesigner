namespace Common.GenerateModels;

public class DataBaseGenerateModel
{
    public string DataBaseName { get; set; } = string.Empty;

    public IEnumerable<TableGenerateModel> Tables { get; set; } = [];

    public IEnumerable<IndexGenerateModel> Indexes { get; set; } = [];
}