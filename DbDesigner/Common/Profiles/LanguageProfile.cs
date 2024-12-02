using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Language;

namespace Common.Profiles;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageDto>();
        CreateMap<LanguageDto, Language>();
        CreateMap<Language, ComboboxDto>();
    }
}