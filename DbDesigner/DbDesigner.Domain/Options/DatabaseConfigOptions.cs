namespace DbDesigner.Domain.Options;

public class DatabaseConfigOptions
{
    public string ContainerName { get; set; } = string.Empty;

    public string ComposePath { get; set; } = string.Empty;

    public Dictionary<string, string> ConnectionStringParams { get; set; }
}