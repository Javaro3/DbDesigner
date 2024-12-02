namespace Common.Dtos.Role;

public class RoleFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
}