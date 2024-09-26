using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Property : BaseModel, IHasId, IHasName, IHasDescription, IHasParams
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool HasParams { get; set; }
    
    public ICollection<Column> Columns { get; set; } = [];
}