using Game.Domain.Common;

namespace Game.Domain.Entities;

public class Player : BaseEntity
{
    public string Username { get; private set; }
    public int Credits { get; private set; }

    private Player() { } // Required by EF

    public Player(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.");

        Username = username;
        Credits = 1000; // Starting credits
    }

    public void AddCredits(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        Credits += amount;
    }
}