using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.User;

namespace Common.Profiles;

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