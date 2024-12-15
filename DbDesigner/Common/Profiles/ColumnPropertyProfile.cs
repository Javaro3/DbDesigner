using AutoMapper;
using Common.Domain;
using Common.Dtos.ColumnProperty;

namespace Common.Profiles;

public class ColumnPropertyProfile : Profile
{
    public ColumnPropertyProfile()
    {
        CreateMap<ColumnProperty, ColumnPropertyDto>();
        CreateMap<ColumnPropertyDto, ColumnProperty>();
        
        CreateMap<ColumnProperty, ColumnPropertyDeleteDto>();
        CreateMap<ColumnPropertyDeleteDto, ColumnProperty>();
        
        CreateMap<ColumnProperty, ColumnPropertyUpdateDto>();
        CreateMap<ColumnPropertyUpdateDto, ColumnProperty>();
    }
}