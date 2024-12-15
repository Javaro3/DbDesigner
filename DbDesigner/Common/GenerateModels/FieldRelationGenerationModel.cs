using Common.Enums;

namespace Common.GenerateModels;

public class FieldRelationGenerationModel
{
    public string RelationDomainName { get; set; } = string.Empty;

    public RelationTypeEnum RelationType;
}