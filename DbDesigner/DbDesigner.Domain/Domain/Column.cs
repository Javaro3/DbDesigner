using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Column : BaseModel, IHasId, IHasName, IHasDescription
{
    public virtual int Id { get; set; }

    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }
    
    public virtual int SqlTypeId { get; set; }
    
    public virtual SqlType? SqlType { get; set; }
    
    public virtual string? SqlTypeParams { get; set; }

    public virtual int TableId { get; set; }
    
    public virtual Table? Table { get; set; }
    
    public virtual ICollection<ColumnProperty> ColumnProperties { get; set; } = [];

    public virtual ICollection<Index> Indices { get; set; } = [];
}