namespace Game.Domain.Common;
using Game.Domain.Entities; // o donde esté BaseEntity

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}