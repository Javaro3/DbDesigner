using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Project : BaseModel, IHasId, IHasName, IHasDescription
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public ICollection<User> Users { get; set; } = [];
    
    public ICollection<Table> Tables { get; set; } = [];
}