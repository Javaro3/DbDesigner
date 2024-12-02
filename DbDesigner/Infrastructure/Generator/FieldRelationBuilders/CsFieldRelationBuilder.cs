using Common.Enums;
using Common.Extensions;
using Common.GenerateModels;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.FieldRelationBuilders;

public class CsFieldRelationBuilder : IFieldRelationBuilder
{
    private string _code = string.Empty;

    public IFieldRelationBuilder AddRelation(FieldRelationGenerationModel model)
    {
        var relationDomainNamePascaleCase = model.RelationDomainName.ConvertToPascalCase();
        
        var type = model.RelationType == RelationTypeEnum.ManyToOne
            ? relationDomainNamePascaleCase
            : $"ICollection<{relationDomainNamePascaleCase}>";
        var name = model.RelationType == RelationTypeEnum.ManyToOne
            ? relationDomainNamePascaleCase
            : relationDomainNamePascaleCase.ToPlural();
        
        var defaultValue = model.RelationType == RelationTypeEnum.ManyToOne
            ? string.Empty
            : $" = new List<{relationDomainNamePascaleCase}>()";
        
        _code = $"\tpublic {type} {name} {{ get; set; }}{defaultValue};";
        return this;
    }
    
    public string Generate()
    {
        return _code;
    }
}