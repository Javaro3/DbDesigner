namespace Common.Dtos.DataBase;

public class DataBaseFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public ICollection<int> IndexTypes { get; set; } = [];
}