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
        var existingUser = await _playerRepository.GetByUsernameAsync(dto.Username, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var existingEmail = await _playerRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (existingEmail != null)
        {
            throw new InvalidOperationException("Email already exists.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var player = new Player(dto.Username, dto.Email, passwordHash);
        
        await _playerRepository.AddAsync(player, cancellationToken);

        return new PlayerDto
        {
            Id = player.Id,
            Username = player.Username,
            Email = player.Email,
            Credits = player.Credits
        };
    }

    public async Task<PlayerDto> LoginAsync(LoginPlayerDto dto, CancellationToken cancellationToken = default)
    {
        var player = await _playerRepository.GetByEmailAsync(dto.Email, cancellationToken);
        if (player == null || !BCrypt.Net.BCrypt.Verify(dto.Password, player.PasswordHash))
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        return new PlayerDto
        {
            Id = player.Id,
            Username = player.Username,
            Email = player.Email,
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
