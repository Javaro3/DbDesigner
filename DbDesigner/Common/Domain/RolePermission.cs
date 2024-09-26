using Common.Domain.BaseDomain;

namespace Common.Domain;

public class RolePermission : BaseModel
{
    public int RoleId { get; set; }
    
    public int PermissionId { get; set; }
}