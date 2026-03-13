using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;

namespace Game.Application.Services;

public interface IPlayerService
{
    Task<PlayerDto> RegisterAsync(RegisterPlayerDto dto, CancellationToken cancellationToken = default);
    Task<PlayerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
