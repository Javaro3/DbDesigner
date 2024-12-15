namespace Common.GenerateModels;

public class ArchitectureGenerateModel
{
    public string Name { get; set; } = string.Empty;

    public List<(string FileName, string FileContent)> InterfaceFiles { get; set; } = [];
    
    public List<(string FileName, string FileContent)> ImplementationFiles { get; set; } = [];
}