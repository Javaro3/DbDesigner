using Common.Extensions;
using Common.GenerateModels;
using Infrastructure.Generator.FieldBuilders;
using Infrastructure.Generator.FieldRelationBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.DomainBuilders;

public class CsDomainBuilder : IDomainBuilder
{
    private string _code = string.Empty;
    
    public IDomainBuilder AddDomainName(string domainName)
    {
        _code = $"public class {domainName.ConvertToPascalCase()} {{\n";
        return this;
    }

    public IDomainBuilder AddFields(IEnumerable<FieldGenerateModel> fields)
    {
        var result = new List<string>();
        foreach (var field in fields)
        {
            var fieldBuilder = new CsFieldBuilder();
            var fieldResult = fieldBuilder
                .AddFieldType(field.FieldType)
                .AddFieldName(field.FieldName)
                .Generate();
            result.Add(fieldResult);
        }

        _code += string.Join("\n\n", result);
        return this;
    }

    public IDomainBuilder AddRelations(IEnumerable<FieldRelationGenerationModel> relations)
    {
        if (relations.Any())
            _code += "\n\n";
        
        var result = new List<string>();
        foreach (var relation in relations)
        {
            var fieldRelationBuilder = new CsFieldRelationBuilder();
            var fieldResult = fieldRelationBuilder
                .AddRelation(relation)
                .Generate();
            result.Add(fieldResult);
        }

        _code += string.Join("\n\n", result);
        return this;
    }

    public string Generate()
    {
        return _code + "\n}";
    }
}