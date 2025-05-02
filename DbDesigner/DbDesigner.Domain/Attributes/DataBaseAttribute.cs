using DbDesigner.Domain.Enums;

namespace DbDesigner.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class DataBaseAttribute : Attribute
{
    public DataBaseEnum DataBase { get; }

    public DataBaseAttribute(DataBaseEnum dataBase)
    {
        DataBase = dataBase;
    }
}