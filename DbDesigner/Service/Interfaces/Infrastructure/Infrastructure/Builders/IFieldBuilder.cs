namespace Service.Interfaces.Infrastructure.Infrastructure.Builders;

public interface IFieldBuilder : IBuilder<string>
{
    IFieldBuilder AddFieldName(string fieldName);

    IFieldBuilder AddFieldType(string type);
}