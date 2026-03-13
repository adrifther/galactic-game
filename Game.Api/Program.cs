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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
