using System;

namespace Game.Application.DTOs;

public class ScoreDto
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid GameSessionId { get; set; }
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SubmitScoreDto
{
    public Guid PlayerId { get; set; }
    public Guid GameSessionId { get; set; }
    public int Points { get; set; }
}
