namespace Common.Dtos.Table;

public class TableBaseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
}