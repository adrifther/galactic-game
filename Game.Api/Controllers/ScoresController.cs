using System;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoresController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitScore([FromBody] SubmitScoreDto dto)
    {
        var result = await _scoreService.SubmitScoreAsync(dto);
        return Ok(result);
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetGlobalLeaderboard([FromQuery] int top = 10)
    {
        var leaderboard = await _scoreService.GetGlobalLeaderboardAsync(top);
        return Ok(leaderboard);
    }
}
