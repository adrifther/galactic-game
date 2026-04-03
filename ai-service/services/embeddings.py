# ai-service/services/embeddings.py
import ollama
from ..config import AI_MODEL

def generate_vector(text):
    """Convierte texto en una lista de 4096 números usando el modelo configurado."""
    response = ollama.embeddings(model=AI_MODEL, prompt=text)
    return response['embedding']
