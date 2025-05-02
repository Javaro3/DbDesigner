using AutoMapper;
using DbDesigner.Application.Dtos.ColumnProperty;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class ColumnPropertyProfile : Profile
{
    public ColumnPropertyProfile()
    {
        CreateMap<ColumnProperty, ColumnPropertyDto>();
        CreateMap<ColumnPropertyDto, ColumnProperty>();
    }
}