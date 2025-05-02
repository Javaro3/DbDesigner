using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class DataBase : BaseModel, IHasId, IHasName, IHasDescription, IHasImage
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }

    public virtual string Image { get; set; } = string.Empty;

    public virtual ICollection<SqlType> SqlTypes { get; set; } = [];
    
    public virtual ICollection<IndexType> IndexTypes { get; set; } = [];

    public virtual ICollection<Project> Projects { get; set; } = [];
}