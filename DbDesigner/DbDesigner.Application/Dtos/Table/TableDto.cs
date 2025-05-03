using DbDesigner.Application.Dtos.Column;
using DbDesigner.Application.Dtos.Index;

namespace DbDesigner.Application.Dtos.Table;

public class TableDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public ICollection<ColumnDto> Columns { get; set; } = [];

    public ICollection<IndexDto> Indexes { get; set; } = [];
}