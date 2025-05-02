using DbDesigner.Application.Dtos.Role;

namespace DbDesigner.Application.Dtos.User;

public class UserAddDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public ICollection<RoleDto> Roles { get; set; } = [];
}