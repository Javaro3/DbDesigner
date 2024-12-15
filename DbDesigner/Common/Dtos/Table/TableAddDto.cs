namespace Common.Dtos.Table;

public class TableAddDto
{
    public int ProjectId { get; set; }
    
    public TableBaseDto? Table { get; set; }
}