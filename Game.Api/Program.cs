using System;
using Game.Infrastructure;
using Game.Domain.Repositories;
using Game.Infrastructure.Repositories;
using Game.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("TU_SECRET_KEY_SUPER_SECRETA_DE_32_CARACTERES")
            )
        };
    });



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174", "http://localhost:5175")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

//Registrar HttpClient de ai-model de python
builder.Services.AddHttpClient("AI", client => {
    client.BaseAddress = new Uri("http://localhost:8000/"); //http://tu-servicio-python.com/
    });

builder.Services.AddHttpClient("SWAPI", client =>
{
    client.BaseAddress = new Uri("https://swapi.dev/api/");
});

// Register your DbContext
builder.Services.AddDbContext<GalacticGameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

// Register Application Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IGameSessionService, GameSessionService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddScoped<IMemoryService, MemoryService>();
builder.Services.AddScoped<IStarWarsDataService, StarWarsDataService>();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// ✅ Endpoint temporal para probar conexión PostgreSQL
app.MapGet("/test-db", async (GalacticGameDbContext db) =>
{
    try
    {
        await db.Database.OpenConnectionAsync();
        await db.Database.CloseConnectionAsync();
        return Microsoft.AspNetCore.Http.Results.Ok("✅ Conexión a la base de datos correcta!");
    }
    catch (Exception ex)
    {
        return Microsoft.AspNetCore.Http.Results.Problem($"❌ Error de conexión: {ex.Message}");
    }
});

app.Run();
