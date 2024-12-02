using Common.Dtos.Column;
using Common.Dtos.Index;

namespace Common.Dtos.Table;

public class TableDiagramDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public ICollection<ColumnDiagramDto> Columns { get; set; } = [];

    public ICollection<IndexDto> Indexes { get; set; } = [];
}