using Common.Domain.BaseDomain;
using Common.Enums;

namespace Common.Domain;

public class Index : BaseModel, IHasId, IHasDescription
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public int IndexTypeId { get; set; }
 
    public IndexType? IndexType { get; set; }

    public ICollection<Column> Columns { get; set; } = [];
}