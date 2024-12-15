namespace Common.Dtos.Project;

public class ProjectFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? CreatedOnFrom { get; set; }
    
    public DateTime? CreatedOnTo { get; set; }
    
    public int? CurrentUser { get; set; }

    public List<int> DataBases { get; set; } = [];
    
    public List<int> Users { get; set; } = [];
}