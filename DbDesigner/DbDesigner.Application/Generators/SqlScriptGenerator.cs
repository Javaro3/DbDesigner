using DbDesigner.Application.Dtos.Project;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Microsoft.Extensions.Options;
using Scriban;

namespace DbDesigner.Application.Generators;

public class SqlScriptGenerator : ISqlScriptGenerator
{
    private readonly SqlGeneratorOptions _sqlGeneratorOptions;

    public SqlScriptGenerator(IOptions<SqlGeneratorOptions> options)
    {
        _sqlGeneratorOptions = options.Value;
    }
    
    public async Task<string> GenerateScriptAsync(ProjectDiagramDto project)
    {
        var generator = new TemplateBuilder(_sqlGeneratorOptions, (DataBaseEnum?)project.DataBase?.Id);
        var sqlTemplate = generator
            .GenerateTable()
            .GenerateColumn()
            .GenerateIndex()
            .GenerateRelation()
            .Build();

        var template = Template.Parse(sqlTemplate);
        var script = await template.RenderAsync(new { Project = project });
        return script;
    }
}