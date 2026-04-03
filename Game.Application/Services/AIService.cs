using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Game.Application.Services;

public class AIService : IAIService
{
    public class EmbeddingResponse
    {
        public float[] embedding { get; set; }
    }
    
    private readonly HttpClient _http;

    public AIService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("AI");
    }

    public async Task<float[]> GetEmbedding(string text)
    {
        var response = await _http.PostAsJsonAsync("embed", new
        {
            text = text
        });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();

        return result.embedding;
    }

    // Envía un evento del juego al servicio de IA (Python)
    public async Task SendGameEventAsync(string text)
    {
        var data = new
        {
            text = text
        };

        await _http.PostAsJsonAsync("save-event", data);
    }

    
}
   
