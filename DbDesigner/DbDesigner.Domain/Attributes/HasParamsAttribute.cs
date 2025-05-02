namespace DbDesigner.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class HasParamsAttribute : Attribute
{
    public bool HasParams { get; }

    public HasParamsAttribute(bool hasParams)
    {
        HasParams = hasParams;
    }
}