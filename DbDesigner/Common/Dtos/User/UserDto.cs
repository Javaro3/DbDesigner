using Common.Dtos.Role;

namespace Common.Dtos.User;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public List<RoleDto> Roles { get; set; } = [];
}