using DbDesigner.Application.Dtos.ColumnProperty;
using DbDesigner.Application.Dtos.Table;

namespace DbDesigner.Application.Dtos.Column;

public class ColumnDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int? SqlTypeId { get; set; }

    public string SqlTypeName { get; set; } = string.Empty;
    
    public string? SqlTypeParams { get; set; }

    public bool SqlTypeHasParams { get; set; }

    public int TableId { get; set; }

    public string TableName { get; set; } = string.Empty;

    public ICollection<ColumnPropertyDto> Properties { get; set; } = [];
}