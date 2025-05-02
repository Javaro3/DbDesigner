using DbDesigner.Application.Dtos.Column;
using DbDesigner.Application.Dtos.RelationAction;

namespace DbDesigner.Application.Dtos.Relation;

public class RelationDto
{
    public int Id { get; set; }
    
    public ColumnDto? SourceColumn { get; set; }
    
    public ColumnDto? TargetColumn { get; set; }
    
    public int SourceColumnId { get; set; }
    
    public int TargetColumnId { get; set; }

    public string OnDeleteName { get; set; } = string.Empty;
    
    public int OnDeleteId { get; set; }

    public string OnUpdateName { get; set; } = string.Empty;

    public int OnUpdateId { get; set; }
}