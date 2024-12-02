using Infrastructure.Generator.Factories;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.RelationBuilders;

public class PostgreSqlRelationBuilder : IRelationBuilder
{
    private string _script = string.Empty;
    
    public IRelationBuilder AddRelationName(string relationName)
    {
        _script = $"\tCONSTRAINT {relationName} ";
        return this;
    }

    public IRelationBuilder AddTarget(string targetColumnName)
    {
        _script += $"FOREIGN KEY ({targetColumnName}) ";
        return this;
    }

    public IRelationBuilder AddSource(string sourceTableName, string sourceColumnName)
    {
        _script += $"REFERENCES {sourceTableName}({sourceColumnName}) ";
        return this;
    }

    public IRelationBuilder AddRelationActions(int onDelete, int onUpdate)
    {
        var relationActionFactory = new RelationActionFactory();
        var onDeleteMssql = relationActionFactory.GetEntity(onDelete);
        var onUpdateMssql = relationActionFactory.GetEntity(onUpdate);

        _script += $"ON DELETE {onDeleteMssql} ON UPDATE {onUpdateMssql}";
        return this;
    }
    
    public string Generate()
    {
        return $"{_script},\n";
    }
}