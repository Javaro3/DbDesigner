using Common.Domain.BaseDomain;

namespace Common.Domain;

public class ColumnProperty : BaseModel
{
    public int ColumnId { get; set; }

    public int PropertyId { get; set; }
    
    public string? PropertyParams { get; set; }
}