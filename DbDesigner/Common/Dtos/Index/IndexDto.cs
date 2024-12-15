namespace Common.Dtos.Index;

public class IndexDto
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public int IndexTypeId { get; set; }

    public ICollection<int> Columns { get; set; } = [];
}