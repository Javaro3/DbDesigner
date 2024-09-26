using Common.Domain.BaseDomain;

namespace Common.Domain;

public class SqlType : BaseModel, IHasId, IHasName, IHasDescription, IHasParams
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public bool HasParams { get; set; }
    
    public ICollection<Column> Columns { get; set; } = [];
    
    public ICollection<DataBase> DataBases { get; set; } = [];
    
    public ICollection<LanguageType> LanguageTypes { get; set; } = [];
}