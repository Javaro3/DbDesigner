from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from models import DataGeneratorRequestDto
from llm_manager import llm_manager
from prompt_manager import prompt_manager
from concurrent.futures import ThreadPoolExecutor
from itertools import repeat


app = FastAPI()
llm_sender = llm_manager()
prompt_generator = prompt_manager()

app.add_middleware(
  CORSMiddleware,
  allow_origins=["https://localhost:7058"],
  allow_credentials=True,
  allow_methods=["*"],
  allow_headers=["*"],
)

@app.post("/generate")
def generate(data: DataGeneratorRequestDto):
  try:
    models = prompt_generator.create_prompt_models(data)
    with ThreadPoolExecutor(max_workers=5) as executor:
        results = list(executor.map(generate_test_data, models, repeat(data.generationModelId)))

    return {"script": "\n\n".join(results), "errors": None}
  except:
     return {"script": None, "errors": "Unexpected error during data generation"}

def generate_test_data(model, generationModelId):
  prompt = prompt_generator.generate_prompt(model)
  return llm_sender.send_request(prompt, generationModelId)