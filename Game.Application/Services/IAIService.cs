public interface IAIService
{
    Task<float[]> GetEmbedding(string text);
    Task SendGameEventAsync(string text);
}