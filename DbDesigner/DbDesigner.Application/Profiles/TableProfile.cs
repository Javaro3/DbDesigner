using AutoMapper;
using DbDesigner.Application.Dtos.Table;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, TableDto>();
        CreateMap<TableDto, Table>();
    }
}