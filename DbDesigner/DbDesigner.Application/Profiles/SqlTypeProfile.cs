using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.SqlType;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class SqlTypeProfile : Profile
{
    public SqlTypeProfile()
    {
        CreateMap<SqlType, SqlTypeDto>();
        CreateMap<SqlTypeDto, SqlType>();
        CreateMap<SqlType, HasParamsComboboxDto>();
    }
}