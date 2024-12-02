using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Role;

namespace Common.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();
        CreateMap<Role, ComboboxDto>();
    }
}