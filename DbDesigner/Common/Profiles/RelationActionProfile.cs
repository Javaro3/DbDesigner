using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.RelationAction;

namespace Common.Profiles;

public class RelationActionProfile : Profile
{
    public RelationActionProfile()
    {
        CreateMap<RelationAction, RelationActionDto>();
        CreateMap<RelationActionDto, RelationAction>();
        CreateMap<RelationAction, ComboboxDto>();

    }
}