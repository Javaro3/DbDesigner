using AutoMapper;
using DbDesigner.Application.Dtos.Column;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class ColumnProfile : Profile
{
    public ColumnProfile()
    {
        CreateMap<Column, ColumnDto>()
            .ForMember(desc => desc.Properties, opt => opt.MapFrom(src => src.ColumnProperties));
        CreateMap<ColumnDto, Column>();
    }
}