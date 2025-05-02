using DbDesigner.Domain.Domain.BaseDomain;

namespace DbDesigner.Domain.Domain;

public class GenerationLanguage : BaseModel, IHasId, IHasName
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}