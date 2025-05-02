using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Architecture;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class ArchitectureProfile : Profile
{
    public ArchitectureProfile()
    {
        CreateMap<Architecture, ArchitectureDto>();
        CreateMap<ArchitectureDto, Architecture>();
        
        CreateMap<Architecture, ComboboxDto>();
        CreateMap<ComboboxDto, Architecture>();
    }
}