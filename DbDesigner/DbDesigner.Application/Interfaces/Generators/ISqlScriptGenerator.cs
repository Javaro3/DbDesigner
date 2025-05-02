using DbDesigner.Application.Dtos.Project;

namespace DbDesigner.Application.Interfaces.Generators;

public interface ISqlScriptGenerator
{
    Task<string> GenerateScriptAsync(ProjectDiagramDto project);
}