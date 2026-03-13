using Game.Domain;
using Game.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure
{
    public class GalacticGameDbContext : DbContext
    {
        public GalacticGameDbContext(DbContextOptions<GalacticGameDbContext> options)
            : base(options) { }

        protected GalacticGameDbContext() { }

        public DbSet<Player> Players => Set<Player>();
        public DbSet<GameSession> GameSessions => Set<GameSession>();
        public DbSet<Score> Scores => Set<Score>();
        public DbSet<Planet> Planets => Set<Planet>();
        public DbSet<Spaceship> Spaceships => Set<Spaceship>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GalacticGameDbContext).Assembly);
        }
    }
}