using Game.Domain;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure
{
    public class GalacticGameDbContext : DbContext
    {
        public GalacticGameDbContext(DbContextOptions<GalacticGameDbContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<GameSession> GameSessions { get; set; } = null!;
        public DbSet<Score> Scores { get; set; } = null!;
    }
}
