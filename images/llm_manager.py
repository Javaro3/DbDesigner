from enums import GenerationModelsEnum
from g4f.client import Client

class llm_manager:
  def send_request(self, generationModelId, prompt):
    if generationModelId == GenerationModelsEnum.ChatGpt:
      client = Client()
      response = client.chat.completions.create(
          model="gpt-4o",
          messages=[{"role": "user", "content": prompt}],
      )
      return response.choices[0].message
    
    elif generationModelId == GenerationModelsEnum.DeepSeekV3:
      client = Client()
      response = client.chat.completions.create(
          model="gpt-4o",
          messages=[{"role": "user", "content": prompt}],
      )
      return response.choices[0].message




if __name__ == "__main__":
  llm = llm_manager()
  response = llm.send_request(GenerationModelsEnum.ChatGpt, "hello")
  print(response)