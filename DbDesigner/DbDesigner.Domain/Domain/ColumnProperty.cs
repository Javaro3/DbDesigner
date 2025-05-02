using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class ColumnProperty : BaseModel, IHasId
{
    public virtual int Id { get; set; }

    public virtual int ColumnId { get; set; }
    
    public virtual Column? Column { get; set; }

    public virtual int PropertyId { get; set; }
    
    public virtual Property? Property { get; set; }
    
    public virtual string? PropertyParams { get; set; }
}