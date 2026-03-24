using System;

namespace Game.Application.DTOs;

public class PlanetDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int ResourceValue { get; set; }
}

public class SpaceshipDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid PlayerId { get; set; }
}
