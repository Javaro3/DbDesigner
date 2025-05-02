namespace DbDesigner.Application.Dtos.Index;

public class IndexDto
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public int IndexTypeId { get; set; }

    public string IndexTypeName { get; set; } = string.Empty;
    
    public string TableName { get; set; } = string.Empty;

    public ICollection<int> Columns { get; set; } = [];

    public ICollection<string> ColumnNames { get; set; } = [];
}