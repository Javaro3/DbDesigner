namespace DbDesigner.Application.Interfaces.Generators;

public interface ITemplateBuilder
{
    ITemplateBuilder GenerateTable();

    ITemplateBuilder GenerateColumn();

    ITemplateBuilder GenerateIndex();

    ITemplateBuilder GenerateRelation();

    string Build();
}