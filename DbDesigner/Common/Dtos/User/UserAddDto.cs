using Common.Dtos.Role;

namespace Common.Dtos.User;

public class UserAddDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public ICollection<RoleDto> Roles { get; set; } = [];
}