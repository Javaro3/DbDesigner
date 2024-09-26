using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Column : BaseModel, IHasId, IHasName, IHasDescription
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int SqlTypeId { get; set; }
    
    public SqlType? SqlType { get; set; }
    
    public string? SqlTypeParams { get; set; }

    public ICollection<Table> Tables { get; set; } = [];
    
    public ICollection<Property> Properties { get; set; } = [];

    public ICollection<Index> Indices { get; set; } = [];
}