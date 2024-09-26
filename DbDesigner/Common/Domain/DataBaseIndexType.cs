using Common.Domain.BaseDomain;

namespace Common.Domain;

public class DataBaseIndexType : BaseModel
{
    public int DataBaseId { get; set; }
    
    public int IndexTypeId { get; set; }
}