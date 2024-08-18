using AutoMapper;
using Common.Domain;
using Common.Dtos.UserDtos;

namespace Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserRegisterDto>();
        CreateMap<UserRegisterDto, User>();
        
        CreateMap<User, UserLoginDto>();
        CreateMap<UserLoginDto, User>();
    }
}