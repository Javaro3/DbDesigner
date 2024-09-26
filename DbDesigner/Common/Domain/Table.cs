using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Table : BaseModel, IHasId, IHasName, IHasDescription
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public ICollection<Project> Projects { get; set; } = [];

    public ICollection<Column> Columns { get; set; } = [];
}