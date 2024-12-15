using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories.MySqlFactories;

public class MySqlPropertyFactory : IFactory<string>
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)PropertyEnum.NotNull => "NOT NULL",
            (int)PropertyEnum.Default => "DEFAULT",
            (int)PropertyEnum.PrimaryKey => "PRIMARY KEY",
            (int)PropertyEnum.Increment => "AUTO_INCREMENT",
            (int)PropertyEnum.Unique => "UNIQUE",
            (int)PropertyEnum.Check => "CHECK",
            _ => string.Empty
        };
    }
}