from fastapi import FastAPI
from pydantic import BaseModel
import requests

app = FastAPI()

OLLAMA_URL = "http://localhost:11434/api/embeddings"
MODEL = "nomic-embed-text"

memory_store = []

class EventRequest(BaseModel):
    text: str

@app.post("/save-event")
def save_event(req: EventRequest):
    response = requests.post(
        OLLAMA_URL,
        json={
            "model": MODEL,
            "prompt": req.text
        }
    )

    embedding = response.json()["embedding"]

    memory_store.append({
        "text": req.text,
        "embedding": embedding
    })

    return {"status": "ok"}