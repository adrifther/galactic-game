using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;

namespace Game.Application.Services;

public interface IGameSessionService
{
    Task<GameSessionDto> StartSessionAsync(StartSessionDto dto, CancellationToken cancellationToken = default);
    Task<GameSessionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task EndSessionAsync(Guid id, CancellationToken cancellationToken = default);
}
