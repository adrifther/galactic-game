using Game.Domain.Common;

namespace Game.Domain.Entities;

public class Planet : BaseEntity
{
    public string Name { get; private set; }
    public int ResourceValue { get; private set; }

    private Planet() { }

    public Planet(string name, int resourceValue)
    {   
        if (resourceValue < 0)
            throw new ArgumentException("Resource value cannot be negative.");
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Planet name cannot be empty.");

        Name = name;
        ResourceValue = resourceValue;
    }
}