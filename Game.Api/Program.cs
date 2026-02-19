using Game.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register your DbContext
builder.Services.AddDbContext<GalacticGameDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Map your endpoints (placeholder)
app.MapGet("/", () => "Galactic Game API running!");

// ✅ Endpoint temporal para probar conexión PostgreSQL
app.MapGet("/test-db", async (GalacticGameDbContext db) =>
{
    try
    {
        await db.Database.OpenConnectionAsync();
        await db.Database.CloseConnectionAsync();
        return Results.Ok("✅ Conexión a la base de datos correcta!");
    }
    catch (Exception ex)
    {
        return Results.Problem($"❌ Error de conexión: {ex.Message}");
    }
});

app.Run();
