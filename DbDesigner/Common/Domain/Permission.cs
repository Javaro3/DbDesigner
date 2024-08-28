namespace Common.Domain;

public class Permission : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = [];
}