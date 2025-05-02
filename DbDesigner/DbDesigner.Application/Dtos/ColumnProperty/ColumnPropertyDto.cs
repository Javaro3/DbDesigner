namespace DbDesigner.Application.Dtos.ColumnProperty;

public class ColumnPropertyDto
{
    public int Id { get; set; }

    public string? PropertyParams { get; set; }

    public int ColumnId { get; set; }

    public int PropertyId { get; set; }

    public string PropertyName { get; set; } = string.Empty;
    
    public bool PropertyHasParams { get; set; }
}