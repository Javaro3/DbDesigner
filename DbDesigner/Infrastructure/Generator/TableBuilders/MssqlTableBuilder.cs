using Common.GenerateModels;
using Infrastructure.Generator.ColumnBuilders;
using Infrastructure.Generator.RelationBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.TableBuilders;

public class MssqlTableBuilder : ITableBuilder
{
    private string _script = string.Empty;
    
    public ITableBuilder AddName(string tableName)
    {
        _script = $"CREATE TABLE {tableName} (\n";
        return this;
    }

    public ITableBuilder AddColumns(IEnumerable<ColumnGenerateModel> columns)
    {
        foreach (var column in columns)
        {
            var columnBuilder = new MssqlColumnBuilder();
            _script += columnBuilder
                .AddName(column.ColumnName)
                .AddSqlType(column.SqlType)
                .AddProperties(column.Properties)
                .Generate();
        }

        return this;
    }

    public ITableBuilder AddRelations(IEnumerable<RelationGenerateModel> relations)
    {
        foreach (var relation in relations)
        {
            var relationBuilder = new MssqlRelationBuilder();
            _script += relationBuilder
                .AddRelationName(relation.RelationName)
                .AddTarget(relation.TargetColumnName)
                .AddSource(relation.SourceTableName, relation.SourceColumnName)
                .AddRelationActions(relation.OnDelete, relation.OnUpdate)
                .Generate();
        }

        return this;
    }

    public string Generate()
    {
        var lastCommaIndex = _script.LastIndexOf(',');
        
        _script = lastCommaIndex != -1 
            ? _script.Remove(lastCommaIndex, 1)
            : _script;
        return _script + ");\n";
    }
}