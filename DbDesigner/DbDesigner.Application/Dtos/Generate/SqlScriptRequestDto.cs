namespace DbDesigner.Application.Dtos.Generate;

public class SqlScriptRequestDto
{
    public string Script { get; set; } = string.Empty;

    public int ProjectId { get; set; }
}