using Common.Domain.BaseDomain;

namespace Common.Domain;

public class IndexColumn : BaseModel
{
    public int IndexId { get; set; }
    
    public int ColumnId { get; set; }
}