using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories.MssqlFactories;

public class MssqlIndexTypeFactory : IFactory<string>
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)IndexTypeEnum.ClusteredIndex => "CLUSTERED INDEX",
            (int)IndexTypeEnum.NonClusteredIndex => "NONCLUSTERED INDEX",
            (int)IndexTypeEnum.UniqueIndex => "UNIQUE INDEX",
            (int)IndexTypeEnum.FullTextIndex => "FULLTEXT INDEX",
            _ => string.Empty
        };
    }
}