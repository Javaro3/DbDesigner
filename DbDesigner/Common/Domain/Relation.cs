using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Relation : BaseModel, IHasId
{
    public int Id { get; set; }
    
    public int SourceColumnId { get; set; }
    
    public Column? SourceColumn { get; set; }

    public int TargetColumnId { get; set; }
    
    public Column? TargetColumn { get; set; }

    public int OnDeleteId { get; set; }
    
    public RelationAction? OnDelete { get; set; }
    
    public int OnUpdateId { get; set; }
    
    public RelationAction? OnUpdate { get; set; }
}