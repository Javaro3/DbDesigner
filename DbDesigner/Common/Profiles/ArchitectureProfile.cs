using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Architecture;

namespace Common.Profiles;

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