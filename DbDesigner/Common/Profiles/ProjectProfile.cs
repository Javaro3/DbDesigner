using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Project;
namespace Common.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectCardDto>();
        CreateMap<ProjectCardDto, Project>();

        CreateMap<Project, ProjectDiagramDto>();
        CreateMap<ProjectDiagramDto, Project>();
        
        CreateMap<Project, ComboboxDto>();
    }
}