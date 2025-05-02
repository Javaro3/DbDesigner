using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class UserRole : BaseModel
{
    public virtual int UserId { get; set; }
    
    public virtual int RoleId { get; set; }
}