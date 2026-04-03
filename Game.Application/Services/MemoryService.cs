using System;
using System.Threading.Tasks;
using System.Text.Json;
using Game.Domain.Repositories;
using Game.Domain.Entities;
using Game.Application.Services;

public class MemoryService : IMemoryService
{
    private readonly IRepository<GameMemory> _repository;

    public MemoryService(IRepository<GameMemory> repository)
    {
        _repository = repository;
    }

    public async Task SaveMemoryAsync(string text, float[] embedding)
    {
        // 1. Crear la instancia del modelo para la base de datos SQL (sólo una vez!)
        var memory = new GameMemory
        {
            EventText = text,
            EmbeddingJson = JsonSerializer.Serialize(embedding),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(memory);
    }
}