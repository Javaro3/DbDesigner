using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Property;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyDto>();
        CreateMap<PropertyDto, Property>();
        CreateMap<Property, HasParamsComboboxDto>();
    }
}