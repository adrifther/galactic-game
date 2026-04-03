using Game.Domain.Common;

namespace Game.Domain.Entities;

public class GameMemory : BaseEntity
{
    public string EventText { get; set; }
    public string EmbeddingJson { get; set; } // luego vector real //// En Postgres esto será tipo 'text' o 'jsonb'
    public DateTime CreatedAt { get; set; }
}

