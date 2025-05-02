using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum GenerationLanguageEnum
{
    [Name("English")]
    English = 1,

    [Name("Spanish")]
    Spanish = 2,

    [Name("French")]
    French = 3,

    [Name("Russian")]
    Russian = 4
}