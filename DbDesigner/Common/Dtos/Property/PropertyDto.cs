namespace Common.Dtos.Property;

public class PropertyDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool HasParams { get; set; }
}