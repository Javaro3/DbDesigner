using Common.Extensions;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.FieldBuilders;

public class CsFieldBuilder : IFieldBuilder
{
    private string _code = string.Empty;

    public IFieldBuilder AddFieldType(string type)
    {
        _code = $"\tpublic {type} ";
        return this;
    }
    
    public IFieldBuilder AddFieldName(string fieldName)
    {
        _code += $"{fieldName.ConvertToPascalCase()} {{ get; set; }}";
        return this;
    }

    public string Generate()
    {
        return _code;
    }
}