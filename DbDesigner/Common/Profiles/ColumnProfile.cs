using AutoMapper;
using Common.Domain;
using Common.Dtos.Column;

namespace Common.Profiles;

public class ColumnProfile : Profile
{
    public ColumnProfile()
    {
        CreateMap<Column, ColumnDiagramDto>();
        CreateMap<ColumnDiagramDto, Column>();
        
        CreateMap<Column, ColumnBaseDto>();
        CreateMap<ColumnBaseDto, Column>();
    }
}