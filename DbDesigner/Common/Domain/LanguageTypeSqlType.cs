using Common.Domain.BaseDomain;

namespace Common.Domain;

public class LanguageTypeSqlType : BaseModel
{
    public int LanguageTypeId { get; set; }
    
    public int SqlTypeId { get; set; }
}