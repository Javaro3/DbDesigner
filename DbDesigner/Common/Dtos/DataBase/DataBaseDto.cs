using Common.Dtos.IndexType;

namespace Common.Dtos.DataBase;

public class DataBaseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public string Image { get; set; } = string.Empty;
    
    public ICollection<IndexTypeDto> IndexTypes { get; set; } = [];
}