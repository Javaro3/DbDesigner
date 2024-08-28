namespace Common.Options;

public class AuthOptions
{
    public ICollection<RolePermissionOptions> RolePermissions { get; set; } = [];
}

public class RolePermissionOptions
{
    public string Role { get; set; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}