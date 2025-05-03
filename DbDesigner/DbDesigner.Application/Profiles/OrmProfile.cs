using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class OrmProfile : Profile
{
    public OrmProfile()
    {
        CreateMap<Orm, OrmDto>();
        CreateMap<OrmDto, Orm>();
        CreateMap<Orm, ComboboxDto>();
    }
}