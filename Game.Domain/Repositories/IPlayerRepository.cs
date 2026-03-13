using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Domain.Entities;

namespace Game.Domain.Repositories;

public interface IPlayerRepository : IRepository<Player>
{
    Task<Player?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}