using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IDataBaseBuilder : IBuilder<string>
{
    IDataBaseBuilder AddName(string dataBaseName);

    IDataBaseBuilder AddTables(IEnumerable<TableGenerateModel> tables);

    IDataBaseBuilder AddIndexes(IEnumerable<IndexGenerateModel> indexes);
}