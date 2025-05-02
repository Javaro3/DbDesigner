using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.RelationAction;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class RelationActionProfile : Profile
{
    public RelationActionProfile()
    {
        CreateMap<RelationAction, RelationActionDto>();
        CreateMap<RelationActionDto, RelationAction>();
        CreateMap<RelationAction, ComboboxDto>();

    }
}