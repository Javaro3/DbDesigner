using Common.Domain.BaseDomain;

namespace Common.Domain;

public class LanguageOrm : BaseModel
{
    public int LanguageId { get; set; }

    public int OrmId { get; set; }
}