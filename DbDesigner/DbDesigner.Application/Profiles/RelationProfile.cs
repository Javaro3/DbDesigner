using AutoMapper;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class RelationProfile : Profile
{
    public RelationProfile()
    {
        CreateMap<Relation, RelationDto>();
        CreateMap<RelationDto, Relation>();
    }
}