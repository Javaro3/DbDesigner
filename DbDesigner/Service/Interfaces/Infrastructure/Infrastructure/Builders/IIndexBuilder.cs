namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IIndexBuilder : IBuilder<string>
{
    IIndexBuilder AddIndexType(int indexType);
    
    IIndexBuilder AddIndexName(string indexName);
    
    IIndexBuilder AddIndexData(string tableName, IEnumerable<string> columns);
}