namespace Common.Dtos.Column;

public class ColumnAddDto
{
    public int TableId { get; set; }
    
    public ColumnBaseDto? Column { get; set; }
}