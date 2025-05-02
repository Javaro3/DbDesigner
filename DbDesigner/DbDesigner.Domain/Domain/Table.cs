using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Table : BaseModel, IHasId, IHasName, IHasDescription
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }

    public virtual int ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = [];
}