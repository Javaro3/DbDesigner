using Common.Enums;
using Infrastructure.Generator.OrmBuilers;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories;

public class OrmBuilderFactory : IFactory<IOrmBuilder>
{
    public IOrmBuilder GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)OrmEnum.EntityFramework => new EntityFrameworkOrmBuilder(),
            _ => new EntityFrameworkOrmBuilder()
        };
    }
}