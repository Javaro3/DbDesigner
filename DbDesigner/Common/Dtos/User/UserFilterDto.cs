namespace Common.Dtos.User;

public class UserFilterDto : FilterRequestDto
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedOnFrom { get; set; }
    
    public DateTime? CreatedOnTo { get; set; }

    public List<int> Roles { get; set; } = [];
}