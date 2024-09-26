using Common.Domain.BaseDomain;

namespace Common.Domain;

public class UserProject : BaseModel
{
    public int UserId { get; set; }
    
    public int ProjectId { get; set; }
}