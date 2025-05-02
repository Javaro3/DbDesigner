namespace DbDesigner.Application.Dtos.Project;

public class ProjectFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public int? User { get; set; }

    public List<int> DataBases { get; set; } = [];
}