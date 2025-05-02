using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Language : BaseModel, IHasId, IHasName, IHasDescription, IHasImage
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }

    public virtual string Image { get; set; } = string.Empty;

    public virtual ICollection<Orm> Orms { get; set; } = [];
}