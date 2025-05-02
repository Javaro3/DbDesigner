using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class User : BaseModel, IHasId, IHasName
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty; 
    
    public virtual string PasswordHash { get; set; } = string.Empty;
    
    public virtual string Email { get; set; } = string.Empty;
    
    public virtual DateTime CreatedOn { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = [];
}