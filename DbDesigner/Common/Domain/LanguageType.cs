using Common.Domain.BaseDomain;

namespace Common.Domain;

public class LanguageType : BaseModel, IHasId, IHasName, IHasDescription
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public int LanguageId { get; set; }
    
    public Language? Language { get; set; }
    
    public int SqlTypeId { get; set; }
    
    public SqlType? SqlType { get; set; }
}