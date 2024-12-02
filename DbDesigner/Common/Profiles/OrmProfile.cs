using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Orm;

namespace Common.Profiles;

public class OrmProfile : Profile
{
    public OrmProfile()
    {
        CreateMap<Orm, OrmDto>();
        CreateMap<OrmDto, Orm>();
        CreateMap<Orm, ComboboxDto>();
    }
}