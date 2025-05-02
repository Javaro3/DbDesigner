namespace DbDesigner.Application.Dtos;

public class FilterRequestDto
{
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }

    public SortFieldDto? SortField { get; set; }
}