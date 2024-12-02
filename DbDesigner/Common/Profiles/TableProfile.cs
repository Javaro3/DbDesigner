using AutoMapper;
using Common.Domain;
using Common.Dtos.Table;

namespace Common.Profiles;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, TableDiagramDto>();
        CreateMap<TableDiagramDto, Table>();
        
        CreateMap<Table, TableBaseDto>();
        CreateMap<TableBaseDto, Table>();
    }
}