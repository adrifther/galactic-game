using System;
using Game.Domain.Entities;

namespace Game.Application.DTOs;

public class GameSessionDto
{
    public Guid Id { get; set; }
    public GameMode Mode { get; set; }
    public Guid Player1Id { get; set; }
    public Guid? Player2Id { get; set; }
    public DateTime StartedAt { get; set; }
    public bool Finished { get; set; }
}

public class StartSessionDto
{
    public GameMode Mode { get; set; }
    public Guid Player1Id { get; set; }
    public Guid? Player2Id { get; set; }
}
