using Common.Dtos.Column;

namespace Common.Dtos.Relation;

public class RelationDto
{
    public int Id { get; set; }
    
    public ColumnDiagramDto? SourceColumn { get; set; }
    
    public ColumnDiagramDto? TargetColumn { get; set; }
    
    public int SourceColumnId { get; set; }
    
    public int TargetColumnId { get; set; }

    public int OnDeleteId { get; set; }
    
    public int OnUpdateId { get; set; }
}