using Common.Domain.BaseDomain;

namespace Common.Domain;

public class TableColumn : BaseModel
{
    public int TableId { get; set; }
    
    public int ColumnId { get; set; }
}