using System;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IGameSessionService _sessionService;

    public SessionsController(IGameSessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartSessionDto dto)
    {
        var result = await _sessionService.StartSessionAsync(dto);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var session = await _sessionService.GetByIdAsync(id);
        if (session == null) return NotFound();
        return Ok(session);
    }
}
