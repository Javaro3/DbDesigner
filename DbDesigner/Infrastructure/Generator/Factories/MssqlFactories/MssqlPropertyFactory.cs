using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories.MssqlFactories;

public class MssqlPropertyFactory : IFactory<string>
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)PropertyEnum.NotNull => "NOT NULL",
            (int)PropertyEnum.Default => "DEFAULT",
            (int)PropertyEnum.PrimaryKey => "PRIMARY KEY",
            (int)PropertyEnum.Increment => "IDENTITY(1, 1)",
            (int)PropertyEnum.Unique => "UNIQUE",
            (int)PropertyEnum.Check => "CHECK",
            _ => string.Empty
        };
    }
}