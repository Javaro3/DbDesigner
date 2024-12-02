using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IColumnBuilder : IBuilder<string>
{
    IColumnBuilder AddName(string columnName);

    IColumnBuilder AddSqlType(SqlTypeGenerateModel sqlType);

    IColumnBuilder AddProperties(IEnumerable<PropertyGenerateModel> properties);
}