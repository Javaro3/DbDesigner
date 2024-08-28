using Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Auth;

public class PermissionRequirement : IAuthorizationRequirement
{
    public ICollection<PermissionEnum> Permissions { get; set; }

    public PermissionRequirement(ICollection<PermissionEnum> permissions)
    {
        Permissions = permissions;
    }
}