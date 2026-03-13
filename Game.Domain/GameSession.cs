using System;
using Game.Domain.Common;
using Game.Domain.Entities;

namespace Game.Domain;

public class GameSession : BaseEntity
{
    public GameMode Mode { get; private set; }

    public Guid Player1Id { get; private set; }
    public Guid? Player2Id { get; private set; }

    public DateTime StartedAt { get; private set; } = DateTime.UtcNow;
    public bool Finished { get; private set; }

    public Player Player1 { get; private set; } = null!;
    public Player? Player2 { get; private set; }

    protected GameSession() { }

    public GameSession(GameMode mode, Guid player1Id, Guid? player2Id = null)
    {
        Mode = mode;
        Player1Id = player1Id;
        Player2Id = player2Id;
    }
}