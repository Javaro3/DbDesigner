namespace Common.Dtos.ColumnProperty;

public class ColumnPropertyUpdateDto
{
    public int PropertyId { get; set; }
    
    public string? PropertyParams { get; set; }
    
    public int PrevPropertyId { get; set; }
    
    public int ColumnId { get; set; }
}