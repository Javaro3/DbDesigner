using Common.Enums;

namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class DataBaseAttribute : Attribute
{
    public IEnumerable<DataBaseEnum> DataBases { get; }

    public DataBaseAttribute(params DataBaseEnum[] dataBases)
    {
        DataBases = dataBases;
    }
}