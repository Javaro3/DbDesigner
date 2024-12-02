using Common.Enums;
using Infrastructure.Generator.DataBaseBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories;

public class DataBaseBuilderFactory : IFactory<IDataBaseBuilder>
{
    public IDataBaseBuilder GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)DataBaseEnum.Mssql => new MssqlDataBaseBuilder(),
            (int)DataBaseEnum.MySql => new MySqlDataBaseBuilder(),
            (int)DataBaseEnum.PostgreSql => new PostgreSqlDataBaseBuilder(),
            (int)DataBaseEnum.SqLite => new SqLiteDataBaseBuilder(),
            _ => new MssqlDataBaseBuilder()
        };
    }
}