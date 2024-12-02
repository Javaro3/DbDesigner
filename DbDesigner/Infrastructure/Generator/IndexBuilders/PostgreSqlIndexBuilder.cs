using Infrastructure.Generator.Factories.PostgreSqlFactories;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.IndexBuilders;

public class PostgreSqlIndexBuilder : IIndexBuilder
{
    private string _script = string.Empty;
    
    public IIndexBuilder AddIndexType(int indexType)
    {
        var indexTypeFactory = new PostgreSqlIndexTypeFactory();
        var postgreSqlIndexType = indexTypeFactory.GetEntity(indexType);
        _script = $"CREATE {postgreSqlIndexType} ";
        return this;
    }

    public IIndexBuilder AddIndexName(string indexName)
    {
        _script += $"{indexName} ";
        return this;
    }

    public IIndexBuilder AddIndexData(string tableName, IEnumerable<string> columns)
    {
        _script += $"ON {tableName} ({string.Join(", ", columns)})";
        return this;
    }
    
    public string Generate()
    {
        return $"{_script};\n\n";
    }
}