using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Project;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<ProjectDto, Project>();

        CreateMap<Project, ProjectDiagramDto>();
        CreateMap<ProjectDiagramDto, Project>();
        
        CreateMap<Project, ComboboxDto>();
    }
}