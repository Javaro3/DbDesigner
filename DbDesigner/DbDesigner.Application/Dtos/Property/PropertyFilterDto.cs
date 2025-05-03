namespace DbDesigner.Application.Dtos.Property;

public class PropertyFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public bool? HasParams { get; set; }

    public List<int> DataBases { get; set; } = [];
}