from enums import GenerationModelsEnum
import g4f


class llm_manager:
  def send_request(self, prompt, generationModelId):
    models = {
      GenerationModelsEnum.DeepSeekV3: g4f.models.deepseek_v3,
      GenerationModelsEnum.DeepSeekR1: g4f.models.deepseek_r1,
      GenerationModelsEnum.Airoboros: g4f.models.airoboros_70b,
      GenerationModelsEnum.Llama: g4f.models.llama_4_scout,
      GenerationModelsEnum.Qwen: g4f.models.qwen_2_5_coder_32b,
      GenerationModelsEnum.ChatGpt: g4f.models.gpt_4o_mini,
    }
    
    response = g4f.ChatCompletion.create(
      model=models[generationModelId],
      messages=[{"role": "user", "content": prompt}]
    ) 

    return response.split("```sql\n", 1)[-1].split("```", 1)[0] if len(response.split("```sql\n", 1)) > 0 else response