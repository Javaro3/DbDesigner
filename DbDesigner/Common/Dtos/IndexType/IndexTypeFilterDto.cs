namespace Common.Dtos.IndexType;

public class IndexTypeFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public ICollection<int> DataBases { get; set; } = [];
}