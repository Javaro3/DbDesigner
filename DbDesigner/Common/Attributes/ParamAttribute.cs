namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ParamAttribute : Attribute
{
    public bool HasParam { get; }

    public ParamAttribute(bool hasParam)
    {
        HasParam = hasParam;
    }
}