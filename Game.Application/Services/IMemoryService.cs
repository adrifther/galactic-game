public interface IMemoryService
{
    Task SaveMemoryAsync(string text, float[] embedding);
}
