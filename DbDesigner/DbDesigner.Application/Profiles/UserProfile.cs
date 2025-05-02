using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.User;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegisterDto>();
        CreateMap<UserRegisterDto, User>();
        
        CreateMap<User, UserLoginDto>();
        CreateMap<UserLoginDto, User>();
        
        CreateMap<User, UserAddDto>();
        CreateMap<UserAddDto, User>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, ComboboxDto>();
    }
}