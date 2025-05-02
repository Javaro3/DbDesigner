using AutoMapper;
using DbDesigner.Application.Dtos.Index;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class IndexProfile : Profile
{
    public IndexProfile()
    {
        CreateMap<Domain.Domain.Index, IndexDto>()
            .ForMember(
                desc => desc.ColumnNames,
                opt => opt.MapFrom(src => src.Columns.Select(e => e.Name)))
            .ForMember(
                desc => desc.TableName,
                opt => opt.MapFrom(src => src.Columns.Any() ? src.Columns.First().Table!.Name : ""))
            .ForMember(
                desc => desc.Columns,
                opt => opt.MapFrom(src => src.Columns.Select(e => e.Id)));
        CreateMap<IndexDto, Domain.Domain.Index>()
            .ForMember(
                desc => desc.Columns,
                opt => opt.MapFrom(src => src.Columns.Select(e => new Column {Id = e})));
    }
}