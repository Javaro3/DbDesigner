using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IFieldRelationBuilder : IBuilder<string>
{
    IFieldRelationBuilder AddRelation(FieldRelationGenerationModel model);
}