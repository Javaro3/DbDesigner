namespace Common.Domain;

public class User : BaseModel
{
    public string Name { get; set; } = string.Empty; 
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = [];
}