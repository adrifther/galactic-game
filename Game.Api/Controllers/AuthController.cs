using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Game.Application.DTOs;
using Game.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Game.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public AuthController(IPlayerService playerService)
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPlayerDto dto)
    {
        try
        {
            var player = await _playerService.LoginAsync(dto);
            var token = GenerateJwtToken(player);
            return Ok(new { token, user = player });
        }
        catch (InvalidOperationException)
        {
            return Unauthorized("Invalid credentials.");
        }
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        if (User == null)
            return Unauthorized();

        var email = User.FindFirst("email")?.Value;
        var username = User.FindFirst("username")?.Value;

        return Ok(new { email, username });
    }

    private string GenerateJwtToken(PlayerDto player)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("TU_SECRET_KEY_SUPER_SECRETA_DE_32_CARACTERES");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                new Claim("username", player.Username),
                new Claim("email", player.Email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}