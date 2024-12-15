using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories.PostgreSqlFactories;

public class PostgreSqlPropertyFactory : IFactory<string>
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)PropertyEnum.NotNull => "NOT NULL",
            (int)PropertyEnum.Default => "DEFAULT",
            (int)PropertyEnum.PrimaryKey => "PRIMARY KEY",
            (int)PropertyEnum.Increment => "SERIAL",
            (int)PropertyEnum.Unique => "UNIQUE",
            (int)PropertyEnum.Check => "CHECK",
            _ => string.Empty
        };
    }
}