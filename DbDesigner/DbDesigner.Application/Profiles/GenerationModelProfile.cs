using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationModel;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class GenerationModelProfile : Profile
{
    public GenerationModelProfile()
    {
        CreateMap<GenerationModel, GenerationModelDto>();
        CreateMap<GenerationModelDto, GenerationModel>();
        
        CreateMap<GenerationModel, ComboboxDto>();
        CreateMap<ComboboxDto, GenerationModel>();
    }
}