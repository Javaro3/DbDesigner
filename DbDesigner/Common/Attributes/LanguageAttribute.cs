using Common.Enums;

namespace Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class LanguageAttribute : Attribute
{
    public IEnumerable<LanguageEnum> Languages { get; }

    public LanguageAttribute(params LanguageEnum[] dataBases)
    {
        Languages = dataBases;
    }
}