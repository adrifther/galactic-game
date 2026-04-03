using System;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPlayerDto dto)
    {
        try
        {
            var result = await _playerService.RegisterAsync(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var player = await _playerService.GetByIdAsync(id);
        if (player == null) return NotFound();
        return Ok(player);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var players = await _playerService.GetAllAsync();
        return Ok(players);
    }
}
