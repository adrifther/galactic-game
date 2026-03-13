using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Domain.Entities;
using Game.Domain.Repositories;

namespace Game.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PlayerDto> RegisterAsync(RegisterPlayerDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _playerRepository.GetByUsernameAsync(dto.Username, cancellationToken);
        if (existing != null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var player = new Player(dto.Username);
        await _playerRepository.AddAsync(player, cancellationToken);

        return new PlayerDto
        {
            Id = player.Id,
            Username = player.Username,
            Credits = player.Credits
        };
    }

    public async Task<PlayerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var player = await _playerRepository.GetByIdAsync(id, cancellationToken);
        if (player == null) return null;

        return new PlayerDto
        {
            Id = player.Id,
            Username = player.Username,
            Credits = player.Credits
        };
    }
}
