using System;

namespace Game.Application.DTOs;

public class PlanetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ResourceValue { get; set; }
}

public class SpaceshipDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid PlayerId { get; set; }
}
