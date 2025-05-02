using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Relation : BaseModel, IHasId
{
    public virtual int Id { get; set; }
    
    public virtual int SourceColumnId { get; set; }
    
    public virtual Column? SourceColumn { get; set; }

    public virtual int TargetColumnId { get; set; }
    
    public virtual Column? TargetColumn { get; set; }

    public virtual int OnDeleteId { get; set; }
    
    public virtual RelationAction? OnDelete { get; set; }
    
    public virtual int OnUpdateId { get; set; }
    
    public virtual RelationAction? OnUpdate { get; set; }
}