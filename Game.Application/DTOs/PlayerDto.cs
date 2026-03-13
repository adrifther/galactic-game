using System;

namespace Game.Application.DTOs;

public class PlayerDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public int Credits { get; set; }
}

public class RegisterPlayerDto
{
    public string Username { get; set; } = string.Empty;
}
