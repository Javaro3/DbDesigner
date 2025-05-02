using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class Property : BaseModel, IHasId, IHasName, IHasDescription, IHasParams
{
    public virtual int Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string? Description { get; set; }
    
    public virtual bool HasParams { get; set; }

    public virtual int DataBaseId { get; set; }
    
    public virtual DataBase? DataBase { get; set; }

    public virtual ICollection<ColumnProperty> ColumnProperties { get; set; } = [];
}