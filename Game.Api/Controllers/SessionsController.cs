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
        try
        {
            var result = await _sessionService.StartSessionAsync(dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return Problem("Unable to start the session right now.");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var session = await _sessionService.GetByIdAsync(id);
        if (session == null) return NotFound();
        return Ok(session);
    }

    [HttpPost("{id:guid}/turns")]
    public async Task<IActionResult> SubmitTurn(Guid id, [FromBody] SubmitTurnDto dto)
    {
        try
        {
            var result = await _sessionService.SubmitTurnAsync(id, dto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
