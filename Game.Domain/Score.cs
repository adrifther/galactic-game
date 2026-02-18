namespace Game.Domain
{
    public class Score
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameSessionId { get; set; }
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public Player Player { get; set; } = null!;
        public GameSession GameSession { get; set; } = null!;
    }
}
