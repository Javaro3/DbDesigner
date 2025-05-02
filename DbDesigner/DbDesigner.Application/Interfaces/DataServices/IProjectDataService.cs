using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Dtos.Project;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IProjectDataService : IBaseDataService<Project, ProjectDto, ProjectFilterDto, ComboboxDto>
{
    Task<ProjectDiagramDto> GetForDiagramByIdAsync(int id);
    
    Task<SqlScriptGenerateResultDto> GenerateScriptAsync(int projectId);
    
    Task<DalGenerateResultDto> GenerateDalAsync(DalGeneratorRequestDto model);

    Task<string?> ScriptIsValidAsync(SqlScriptRequestDto model);
    
    Task<string?> GetScriptAsync(int projectId);
    
    Task<byte[]> GetProjectFilesAsync(int projectId);
}