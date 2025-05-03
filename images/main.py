from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from models import DataGeneratorRequestDto

app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["https://localhost:7058"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

@app.post("/generate")
def generate(data: DataGeneratorRequestDto):
    print(f"Generating for project: {data.project.name if data.project else 'No project'}")
    print(f"Tables to generate: {len(data.tableGenerateInfos)}")

    return {
      "script": "test script",
      "errors": "test errors"
    }