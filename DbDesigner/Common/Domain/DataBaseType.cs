using Common.Domain.BaseDomain;

namespace Common.Domain;

public class DataBaseType : BaseModel
{
    public int DataBaseId { get; set; }
    
    public int SqlTypeId { get; set; }
}