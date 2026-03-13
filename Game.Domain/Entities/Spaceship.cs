using Game.Domain.Common;

namespace Game.Domain.Entities;

public class Spaceship : BaseEntity
{
    public string Name { get; private set; }
    public int AttackPower { get; private set; }
    public int DefensePower { get; private set; }
    public Guid PlayerId { get; private set; }
    public Player Player { get; private set; }

    private Spaceship() { }

    public Spaceship(string name, int attackPower, int defensePower, Guid playerId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");

        Name = name;
        AttackPower = attackPower;
        DefensePower = defensePower;
        PlayerId = playerId;
    }
}