using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories.SqLiteFactories;

public class SqLiteIndexTypeFactory : IFactory<string> 
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)IndexTypeEnum.UniqueIndex => "UNIQUE INDEX",
            (int)IndexTypeEnum.BTreeIndex => "INDEX",
            _ => string.Empty
        };
    }
}