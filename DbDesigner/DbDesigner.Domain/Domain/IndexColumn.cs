using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class IndexColumn : BaseModel
{
    public virtual int IndexId { get; set; }
    
    public virtual int ColumnId { get; set; }
}