using Common.GenerateModels;
using Infrastructure.Generator.Factories.MySqlFactories;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.IndexBuilders;

public class MySqlIndexBuilder : IIndexBuilder
{
    private string _script = string.Empty;
    
    public IIndexBuilder AddIndexType(int indexType)
    {
        var indexTypeFactory = new MySqlIndexTypeFactory();
        var mySqlIndexType = indexTypeFactory.GetEntity(indexType);
        _script = $"CREATE {mySqlIndexType} ";
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