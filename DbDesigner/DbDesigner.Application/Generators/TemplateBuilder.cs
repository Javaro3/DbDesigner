using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators;

public class TemplateBuilder : ITemplateBuilder
{
    private readonly SqlGeneratorOptions _options;
    private readonly DataBaseEnum? _dataBase;
    private string _template;

    public TemplateBuilder(SqlGeneratorOptions options, DataBaseEnum? dataBase)
    {
        _options = options;
        _dataBase = dataBase;
        _template = _options.GetSqlTemplate();
    }

    public ITemplateBuilder GenerateTable()
    {
        _template = _template.Replace("TABLE", _options.GetTableTemplate(_dataBase));
        return this;
    }

    public ITemplateBuilder GenerateColumn()
    {
        _template = _template.Replace("COLUMN", _options.GetColumnTemplate(_dataBase));
        return this;
    }

    public ITemplateBuilder GenerateIndex()
    {
        _template = _template.Replace("INDEX", _options.GetIndexTemplate(_dataBase));
        return this;
    }

    public ITemplateBuilder GenerateRelation()
    {
        _template = _template.Replace("RELATION", _options.GetRelationTemplate(_dataBase));
        return this;
    }

    public string Build()
    {
        return _template;
    }
}