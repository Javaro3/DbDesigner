using Common.Domain.BaseDomain;

namespace Common.Domain;

public class Language : BaseModel, IHasId, IHasName, IHasDescription, IHasImage
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public string Image { get; set; } = string.Empty;

    public ICollection<Orm> Orms { get; set; } = [];
}