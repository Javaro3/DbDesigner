using AutoMapper;
using Common.Domain;
using Common.Dtos.Relation;

namespace Common.Profiles;

public class RelationProfile : Profile
{
    public RelationProfile()
    {
        CreateMap<Relation, RelationDto>();
        CreateMap<RelationDto, Relation>();
    }
}