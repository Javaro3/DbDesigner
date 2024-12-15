using Common.Enums;
using Infrastructure.Generator.ArchitectureBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories;

public class ArchitectureFactory : IFactory<IArchitectureBuilder?>
{
    public IArchitectureBuilder? GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)ArchitectureEnum.DirectDataAccess => null,
            (int)ArchitectureEnum.RepositoryPattern => new RepositoryBuilder(),
            _ => (IArchitectureBuilder)null
        };
    }
}