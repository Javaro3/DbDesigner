namespace Common.Dtos.RelationAction;

public class RelationActionFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
}