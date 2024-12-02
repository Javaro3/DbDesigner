using Common.Domain;
using Common.Dtos;
using Common.Dtos.Project;
using Common.Dtos.Property;
using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IProjectDataService : IBaseDataService<Project, ProjectCardDto, ProjectFilterDto, ComboboxDto>
{
    Task<ProjectDiagramDto> GetForDiagramByIdAsync(int id);
    
    Task<ResultGenerateModel> GetDataBaseSqlScript(ProjectGenerateDto dto);

}