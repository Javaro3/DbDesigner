using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class DataBaseProfile : Profile
{
    public DataBaseProfile()
    {
        CreateMap<DataBase, DataBaseDto>();
        CreateMap<DataBaseDto, DataBase>();
        CreateMap<DataBase, ComboboxDto>();
    }
}