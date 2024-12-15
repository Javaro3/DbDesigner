namespace Common.GenerateModels;

public class DomainGenerateModel
{
    public int DomainId { get; set; }
    
    public string DomainName { get; set; } = string.Empty;

    public List<FieldGenerateModel> Fields { get; set; } = [];
    
    public List<FieldRelationGenerationModel> Relations { get; set; } = [];

}