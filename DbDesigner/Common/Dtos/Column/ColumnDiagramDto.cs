using Common.Dtos.ColumnProperty;
using Common.Dtos.SqlType;

namespace Common.Dtos.Column;

public class ColumnDiagramDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int? SqlTypeId { get; set; }
    
    public string? SqlTypeParams { get; set; }

    public ICollection<ColumnPropertyDto> Properties { get; set; } = [];
}