using DbDesigner.Domain.Enums;

namespace DbDesigner.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class LanguageAttribute : Attribute
{
    public LanguageEnum Language { get; }

    public LanguageAttribute(LanguageEnum language)
    {
        Language = language;
    }
}