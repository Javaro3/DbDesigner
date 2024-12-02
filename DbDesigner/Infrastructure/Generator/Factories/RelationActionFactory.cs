using Common.Enums;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories;

public class RelationActionFactory : IFactory<string>
{
    public string GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)RelationActionEnum.Cascade => "CASCADE",
            (int)RelationActionEnum.SetNull => "SET NULL",
            (int)RelationActionEnum.SetDefault => "SET DEFAULT",
            (int)RelationActionEnum.Restrict => "RESTRICT",
            (int)RelationActionEnum.NoAction => "NO ACTION",
            _ => string.Empty
        };
    }
}