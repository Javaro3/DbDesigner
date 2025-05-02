using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Role;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();
        CreateMap<Role, ComboboxDto>();
    }
}