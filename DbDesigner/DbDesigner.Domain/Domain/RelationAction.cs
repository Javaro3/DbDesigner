using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class RelationAction : BaseModel, IHasId, IHasName, IHasDescription
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }
}