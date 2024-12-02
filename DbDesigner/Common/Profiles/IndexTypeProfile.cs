using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.IndexType;

namespace Common.Profiles;

public class IndexTypeProfile : Profile
{
    public IndexTypeProfile()
    {
        CreateMap<IndexType, IndexTypeDto>();
        CreateMap<IndexTypeDto, IndexType>();
        CreateMap<IndexType, ComboboxDto>();
    }
}