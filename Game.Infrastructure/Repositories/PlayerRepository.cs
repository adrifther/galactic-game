using System.Threading;
using System.Threading.Tasks;
using Game.Domain.Entities;
using Game.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure.Repositories;

public class PlayerRepository : Repository<Player>, IPlayerRepository
{
    public PlayerRepository(GalacticGameDbContext context) : base(context)
    {
    }

    public async Task<Player?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Players.FirstOrDefaultAsync(p => p.Username == username, cancellationToken);
    }
}
