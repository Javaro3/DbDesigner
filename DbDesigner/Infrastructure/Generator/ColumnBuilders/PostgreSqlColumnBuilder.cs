using Common.GenerateModels;
using Infrastructure.Generator.Factories.PostgreSqlFactories;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.ColumnBuilders;

public class PostgreSqlColumnBuilder : IColumnBuilder
{
    private string _script = string.Empty;
    
    public IColumnBuilder AddName(string columnName)
    {
        _script = $"\t{columnName} ";
        return this;
    }

    public IColumnBuilder AddSqlType(SqlTypeGenerateModel sqlType)
    {
        var result = $"{sqlType.SqlTypeName}";
        if (!string.IsNullOrEmpty(sqlType.Params))
            result += $"({sqlType.Params})";
        _script += $"{result} ";
        return this;
    }

    public IColumnBuilder AddProperties(IEnumerable<PropertyGenerateModel> properties)
    {
        var result = new List<string>();
        var propertyFactory = new PostgreSqlPropertyFactory();
        foreach (var property in properties)
        {
            var propertyResult = propertyFactory.GetEntity(property.Property);
            if (!string.IsNullOrEmpty(property.Params))
                propertyResult += $"{property.Params}";
            result.Add(propertyResult);
        }

        _script += string.Join(" ", result);
        return this;
    }

    public string Generate()
    {
        return _script + ",\n";
    }
}