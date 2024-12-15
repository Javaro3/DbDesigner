using Common.GenerateModels;
using Infrastructure.Generator.IndexBuilders;
using Infrastructure.Generator.TableBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;

namespace Infrastructure.Generator.DataBaseBuilders;

public class SqLiteDataBaseBuilder : IDataBaseBuilder
{
    private string _script = string.Empty;

    public IDataBaseBuilder AddName(string dataBaseName)
    {
        _script = """
                   -- Database creating
                   -- In SQLite, a database is created automatically when you connect to a database file.


                   """;
        return this;
    }

    public IDataBaseBuilder AddTables(IEnumerable<TableGenerateModel> tables)
    {
        _script += "-- Creating tables\n";
        foreach (var table in tables)
        {
            var tableBuilder = new SqLiteTableBuilder();
            _script += tableBuilder
                .AddName(table.TableName)
                .AddColumns(table.Columns)
                .AddRelations(table.Relations)
                .Generate();
            _script += "\n";
        }

        return this;
    }

    public IDataBaseBuilder AddIndexes(IEnumerable<IndexGenerateModel> indexes)
    {
        _script += "-- Creating indexes\n";
        foreach (var index in indexes)
        {
            var indexBuilder = new PostgreSqlIndexBuilder();
            _script += indexBuilder
                .AddIndexType(index.IndexType)
                .AddIndexName(index.IndexName)
                .AddIndexData(index.TableName, index.ColumnNames)
                .Generate();
        }
        
        return this;
    }

    public string Generate()
    {
        return _script;
    }
}