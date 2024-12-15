using Common.Domain.BaseDomain;

namespace Common.Domain;

public class DataBase : BaseModel, IHasId, IHasName, IHasDescription, IHasImage
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public string Image { get; set; } = string.Empty;

    public ICollection<SqlType> SqlTypes { get; set; } = [];
    
    public ICollection<IndexType> IndexTypes { get; set; } = [];

    public ICollection<Project> Projects { get; set; } = [];
}