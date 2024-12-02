using Common.Enums;
using Infrastructure.Generator.DomainBuilders;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;

namespace Infrastructure.Generator.Factories;

public class DomainBuilderFactory : IFactory<IDomainBuilder>
{
    public IDomainBuilder GetEntity(int entityId)
    {
        return entityId switch
        {
            (int)LanguageEnum.CSharp => new CsDomainBuilder(),
            _ => new CsDomainBuilder()
        };
    }
}