using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Language;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageDto>();
        CreateMap<LanguageDto, Language>();
        CreateMap<Language, ComboboxDto>();
    }
}