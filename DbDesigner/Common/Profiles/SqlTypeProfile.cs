using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.SqlType;

namespace Common.Profiles;

public class SqlTypeProfile : Profile
{
    public SqlTypeProfile()
    {
        CreateMap<SqlType, SqlTypeDto>();
        CreateMap<SqlTypeDto, SqlType>();
        CreateMap<SqlType, HasParamsComboboxDto>();
    }
}