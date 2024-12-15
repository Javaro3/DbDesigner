namespace Common.Dtos.Architecture;

public class ArchitectureFilterDto : FilterRequestDto
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
}