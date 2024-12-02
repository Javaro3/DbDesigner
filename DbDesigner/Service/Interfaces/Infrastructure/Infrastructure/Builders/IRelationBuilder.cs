namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IRelationBuilder : IBuilder<string>
{
    IRelationBuilder AddRelationName(string relationName);
    
    IRelationBuilder AddTarget(string targetColumnName);

    IRelationBuilder AddSource(string sourceTableName, string sourceColumnName);
    
    IRelationBuilder AddRelationActions(int onDelete, int onUpdate);
}