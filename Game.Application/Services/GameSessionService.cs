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
    private readonly IAIService _ai;
    private readonly IMemoryService _memory;

    public GameSessionService(IRepository<GameSession> sessionRepository, IAIService ai, IMemoryService memory)
    {
        _sessionRepository = sessionRepository;
        _ai = ai;
        _memory = memory;
    }
    
    public async Task HandlePlanetAttack(string player, string planet)
    {
        var detail = $"{player} attacked {planet}";
        
        // ⚡ no bloquea el juego
        _ = Task.Run(async () => await _ai.GetEmbedding(detail));

        // 1. Guardar en el histórico "tradicional" (SQL)
        /* To Do: Add Attack Entry logic when entity is defined
        var attackLog = new AttackEntry { 
            Player = player, 
            Target = planet, 
            Timestamp = DateTime.UtcNow 
        };

        await _sessionRepository.SaveAttackAsync(attackLog);
        */

        // 2. Enviar a la IA para telemetría o procesamiento posterior
        await _ai.SendGameEventAsync(detail);

        // 3. (Opcional) Si quieres que la IA te diga "qué tan agresivo" fue:
        float[] embedding = await _ai.GetEmbedding(detail);
        
        // 4. Calcular el resultado del ataque
        // var result = await _ai.CalculateAttackResultAsync(detail);
    }

    public async Task<GameSessionDto> StartSessionAsync(StartSessionDto dto, CancellationToken cancellationToken = default)
    {
        var session = new GameSession(dto.Mode, dto.Player1Id, dto.Player2Id);
        
        // Guardamos en Base de Datos
        await _sessionRepository.AddAsync(session, cancellationToken);

        // Notificamos a la IA que empezó una sesión
        await _ai.SendGameEventAsync($"New session started: {session.Id} in mode {dto.Mode}");
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
