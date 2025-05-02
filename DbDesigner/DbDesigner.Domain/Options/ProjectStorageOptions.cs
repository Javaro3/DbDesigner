namespace DbDesigner.Domain.Options;

public class ProjectStorageOptions
{
    public string Path { get; set; } = string.Empty;

    public string DalName { get; set; } = string.Empty;

    public string SqlScriptFileName { get; set; } = string.Empty;
    
    public string DataScriptFileName { get; set; } = string.Empty;
}