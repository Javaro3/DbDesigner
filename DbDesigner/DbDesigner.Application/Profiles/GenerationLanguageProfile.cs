using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationLanguage;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class GenerationLanguageProfile : Profile
{
    public GenerationLanguageProfile()
    {
        CreateMap<GenerationLanguage, GenerationLanguageDto>();
        CreateMap<GenerationLanguageDto, GenerationLanguage>();
        
        CreateMap<GenerationLanguage, ComboboxDto>();
        CreateMap<ComboboxDto, GenerationLanguage>();
    }
}