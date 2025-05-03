using DbDesigner.Domain.Enums;

namespace DbDesigner.Domain.Options;

public class SqlGeneratorOptions
{
    public string BasePath { get; set; } = string.Empty;

    public string GetSqlTemplate() => File.ReadAllText(Path.Combine(BasePath, "sql.scriban"));

    public string GetTableTemplate(DataBaseEnum? dataBase) => GetTemplateByType(dataBase, "table");
    
    public string GetColumnTemplate(DataBaseEnum? dataBase) => GetTemplateByType(dataBase, "column");
    
    public string GetIndexTemplate(DataBaseEnum? dataBase) => GetTemplateByType(dataBase, "index");

    public string GetRelationTemplate(DataBaseEnum? dataBase) => GetTemplateByType(dataBase, "relation");

    private string GetTemplateByType(DataBaseEnum? dataBase, string type)
    {
        if (dataBase is null)
            throw new ArgumentException("Unsupported database");
        return File.ReadAllText(Path.Combine(BasePath, $"{dataBase}/{type}.scriban"));
    }
}