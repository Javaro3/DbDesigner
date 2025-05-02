namespace DbDesigner.Application.Dtos.Generate;

public class SqlScriptGenerateResultDto
{
    public string Script { get; set; } = string.Empty;
    
    public string? Errors { get; set; }
}