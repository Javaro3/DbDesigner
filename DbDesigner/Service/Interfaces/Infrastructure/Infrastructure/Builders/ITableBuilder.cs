using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface ITableBuilder : IBuilder<string>
{
    ITableBuilder AddName(string tableName);
    
    ITableBuilder AddColumns(IEnumerable<ColumnGenerateModel> columns);
    
    ITableBuilder AddRelations(IEnumerable<RelationGenerateModel> relations);

}