# ai-service/services/memory.py
import psycopg2
from ..config import DB_CONFIG
from .embeddings import generate_vector

def save_memory(npc_id, content):
    """Genera el embedding y lo guarda en la base de datos."""
    # 1. Obtener el vector de 4096 dimensiones
    vector = generate_vector(content)
    
    # 2. Conectar a Postgres usando la configuración centralizada
    conn = psycopg2.connect(**DB_CONFIG)
    cur = conn.cursor()
    
    try:
        # 3. Insertar. Psycopg2 se encarga de convertir la lista de Python al vector de PG
        cur.execute(
            "INSERT INTO npc_memory (npc_id, content, embedding) VALUES (%s, %s, %s)",
            (npc_id, content, vector)
        )
        conn.commit()
        print(f"Memoria guardada para el NPC {npc_id}")
    except Exception as e:
        print(f"Error guardando memoria: {e}")
        conn.rollback()
    finally:
        cur.close()
        conn.close()
