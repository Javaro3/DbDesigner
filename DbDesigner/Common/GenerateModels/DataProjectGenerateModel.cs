namespace Common.GenerateModels;

public class DataProjectGenerateModel
{
    public string DatabaseName { get; set; } = string.Empty;

    public List<DataTableGenerateModel> Tables { get; set; } = [];
}