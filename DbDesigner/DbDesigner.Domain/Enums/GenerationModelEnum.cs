using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum GenerationModelEnum
{
    [Name("Gpt-4o mini")]
    ChatGpt = 1,

    [Name("DeepSeek V3")]
    DeepSeekV3 = 2,
    
    [Name("DeepSeek R1")]
    DeepSeekR1 = 3,
    
    [Name("Airoboros")]
    Airoboros = 4,
    
    [Name("Llama 4 Scout")]
    Llama = 5,
    
    [Name("Qwen coder")]
    Qwen = 6
}