from enum import Enum

class GenerationModelsEnum(int, Enum):
    ChatGpt = 1
    DeepSeekV3 = 2
    DeepSeekR1 = 3
    Airoboros = 4
    Llama = 5
    Qwen = 6