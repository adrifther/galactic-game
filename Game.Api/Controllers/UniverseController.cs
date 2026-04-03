using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniverseController : ControllerBase
{
    private readonly IStarWarsDataService _starWarsDataService;
    private readonly ILogger<UniverseController> _logger;

    public UniverseController(IStarWarsDataService starWarsDataService, ILogger<UniverseController> logger)
    {
        _starWarsDataService = starWarsDataService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniverse(CancellationToken cancellationToken)
    {
        try
        {
            var universe = await _starWarsDataService.GetUniverseAsync(false, cancellationToken);
            return Ok(universe);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Universe fetch failed while contacting SWAPI");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "SWAPI is currently unavailable. Try syncing again later.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Universe fetch failed");
            return Problem("Unable to load the cached universe right now.");
        }
    }

    [HttpPost("sync")]
    public async Task<IActionResult> Sync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _starWarsDataService.SyncUniverseAsync(cancellationToken);
            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Universe sync failed while contacting SWAPI");
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "SWAPI sync failed because the external API is unavailable.");
        }
        catch (FormatException ex)
        {
            _logger.LogWarning(ex, "Universe sync failed due to unexpected SWAPI payload");
            return StatusCode(StatusCodes.Status502BadGateway, "SWAPI returned an unexpected response format.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Universe sync failed");
            return Problem("Unable to sync the Star Wars cache right now.");
        }
    }
}
