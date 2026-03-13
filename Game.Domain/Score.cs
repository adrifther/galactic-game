using System;
using Game.Domain.Common;
using Game.Domain.Entities;

namespace Game.Domain
{
    public class Score : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public Guid GameSessionId { get; set; }
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public Player Player { get; set; } = null!;
        public GameSession GameSession { get; set; } = null!;
    }
}
