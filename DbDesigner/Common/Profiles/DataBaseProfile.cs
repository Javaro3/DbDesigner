using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.DataBase;

namespace Common.Profiles;

public class DataBaseProfile : Profile
{
    public DataBaseProfile()
    {
        CreateMap<DataBase, DataBaseDto>();
        CreateMap<DataBaseDto, DataBase>();
        CreateMap<DataBase, ComboboxDto>();
    }
}