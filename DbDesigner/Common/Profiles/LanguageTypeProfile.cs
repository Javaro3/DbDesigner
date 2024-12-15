using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.LanguageType;

namespace Common.Profiles;

public class LanguageTypeProfile : Profile
{
    public LanguageTypeProfile()
    {
        CreateMap<LanguageType, LanguageTypeDto>();
        CreateMap<LanguageTypeDto, LanguageType>();
        CreateMap<LanguageType, ComboboxDto>();
    }
}