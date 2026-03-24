using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Domain;
using Game.Domain.Entities;
using Game.Domain.Repositories;

namespace Game.Application.Services;

public class GameSessionService : IGameSessionService
{
    private readonly IRepository<GameSession> _sessionRepository;

    public GameSessionService(IRepository<GameSession> sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<GameSessionDto> StartSessionAsync(StartSessionDto dto, CancellationToken cancellationToken = default)
    {
        var session = new GameSession(dto.Mode, dto.Player1Id, dto.Player2Id);
        await _sessionRepository.AddAsync(session, cancellationToken);

        return MapToDto(session);
    }

    public async Task<GameSessionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var session = await _sessionRepository.GetByIdAsync(id, cancellationToken);
        return session == null ? null : MapToDto(session);
    }

    public async Task EndSessionAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var session = await _sessionRepository.GetByIdAsync(id, cancellationToken);
        if (session == null) throw new InvalidOperationException("Session not found");

        // Use reflection or a proper End() method on GameSession if available. For now, since Finished is private set, we might not be able to set it directly.
        // I will assume Finished can be updated if we had a method, but since the domain doesn't have it, I'll ignore or wait for compilation.
        // Actually, let's leave this as is - just mapping since Finished has private set. We will just save changes.
        // Wait, without an End() method, we can't change Finished. I'll just leave it no-op or throw.
        throw new NotImplementedException("Domain logic missing for ending session.");
    }

    private static GameSessionDto MapToDto(GameSession session) => new()
    {
        Id = session.Id,
        Mode = session.Mode,
        Player1Id = session.Player1Id,
        Player2Id = session.Player2Id,
        StartedAt = session.StartedAt,
        Finished = session.Finished
    };
}
