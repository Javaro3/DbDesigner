namespace Common.Dtos.Orm;

public class OrmFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public List<int> Languages { get; set; } = [];
}