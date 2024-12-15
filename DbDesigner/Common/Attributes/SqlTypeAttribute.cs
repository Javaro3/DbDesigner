using Common.Enums;

namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class SqlTypeAttribute : Attribute
{
    public IEnumerable<SqlTypeEnum> SqlTypes { get; }

    public SqlTypeAttribute(params SqlTypeEnum[] sqlTypes)
    {
        SqlTypes = sqlTypes;
    }
}