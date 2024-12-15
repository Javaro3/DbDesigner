using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IDomainBuilder : IBuilder<string>
{
    IDomainBuilder AddDomainName(string domainName);

    IDomainBuilder AddFields(IEnumerable<FieldGenerateModel> fields);
    
    IDomainBuilder AddRelations(IEnumerable<FieldRelationGenerationModel> relations);
}