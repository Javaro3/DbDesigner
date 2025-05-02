using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Index : BaseModel, IHasId, IHasDescription
{
    public virtual int Id { get; set; }
    
    public virtual string? Description { get; set; }
    
    public virtual int IndexTypeId { get; set; }
 
    public virtual IndexType? IndexType { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = [];
}