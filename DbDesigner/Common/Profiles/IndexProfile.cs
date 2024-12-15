using AutoMapper;
using Common.Domain;
using Common.Dtos.Index;

namespace Common.Profiles;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Domain.Index, IndexDto>()
            .ForMember(
                desc => desc.Columns,
                opt => opt.MapFrom(src => src.Columns.Select(e => e.Id)));
        CreateMap<IndexDto, Domain.Index>()
            .ForMember(
                desc => desc.Columns,
                opt => opt.MapFrom(src => src.Columns.Select(e => new Column {Id = e})));;
    }
}