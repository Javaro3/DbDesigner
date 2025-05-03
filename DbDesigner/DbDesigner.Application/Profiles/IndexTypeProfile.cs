using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.IndexType;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class IndexTypeProfile : Profile
{
    public IndexTypeProfile()
    {
        CreateMap<IndexType, IndexTypeDto>();
        CreateMap<IndexTypeDto, IndexType>();
        CreateMap<IndexType, ComboboxDto>();
    }
}