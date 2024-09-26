using Common.Domain.BaseDomain;

namespace Common.Domain;

public class User : BaseModel, IHasId, IHasName
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty; 
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; }

    public ICollection<Role> Roles { get; set; } = [];
    
    public ICollection<Project> Projects { get; set; } = [];
}