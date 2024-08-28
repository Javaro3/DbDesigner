namespace Common.Domain;

public class Role : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Permission> Permissions { get; set; } = [];
    
    public ICollection<User> Users { get; set; } = [];
}