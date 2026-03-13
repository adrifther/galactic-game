using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.Application.DTOs;

namespace Game.Application.Services;

public interface IScoreService
{
    Task<ScoreDto> SubmitScoreAsync(SubmitScoreDto dto, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ScoreDto>> GetGlobalLeaderboardAsync(int top = 10, CancellationToken cancellationToken = default);
}
