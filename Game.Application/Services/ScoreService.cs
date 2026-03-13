using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;
using Game.Domain;
using Game.Domain.Repositories;

namespace Game.Application.Services;

public class ScoreService : IScoreService
{
    private readonly IRepository<Score> _scoreRepository;

    public ScoreService(IRepository<Score> scoreRepository)
    {
        _scoreRepository = scoreRepository;
    }

    public async Task<ScoreDto> SubmitScoreAsync(SubmitScoreDto dto, CancellationToken cancellationToken = default)
    {
        var score = new Score
        {
            PlayerId = dto.PlayerId,
            GameSessionId = dto.GameSessionId,
            Points = dto.Points
        };

        await _scoreRepository.AddAsync(score, cancellationToken);

        return new ScoreDto
        {
            Id = score.Id,
            PlayerId = score.PlayerId,
            GameSessionId = score.GameSessionId,
            Points = score.Points,
            CreatedAt = score.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ScoreDto>> GetGlobalLeaderboardAsync(int top = 10, CancellationToken cancellationToken = default)
    {
        var allScores = await _scoreRepository.GetAllAsync(cancellationToken);
        
        return allScores
            .OrderByDescending(s => s.Points)
            .Take(top)
            .Select(s => new ScoreDto
            {
                Id = s.Id,
                PlayerId = s.PlayerId,
                GameSessionId = s.GameSessionId,
                Points = s.Points,
                CreatedAt = s.CreatedAt
            })
            .ToList();
    }
}
