using DbDesigner.Application.Dtos.Role;

namespace DbDesigner.Application.Dtos.User;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public List<RoleDto> Roles { get; set; } = [];
}