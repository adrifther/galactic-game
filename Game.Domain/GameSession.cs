namespace Game.Domain
{
    public enum GameMode
    {
        PvP,
        PvE
    }

    public class GameSession
    {
        public int Id { get; set; }
        public GameMode Mode { get; set; }
        public int Player1Id { get; set; }
        public int? Player2Id { get; set; } // null si es PvE
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public bool Finished { get; set; } = false;

        // Relaciones
        public Player Player1 { get; set; } = null!;
        public Player? Player2 { get; set; }
    }
}
