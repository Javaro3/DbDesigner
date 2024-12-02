using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Property;

namespace Common.Profiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyDto>();
        CreateMap<PropertyDto, Property>();
        CreateMap<Property, HasParamsComboboxDto>();
    }
}