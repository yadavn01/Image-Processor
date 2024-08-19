public interface IImageRepository
{
    Task SaveJsonAsync(string fileName, string jsonContent);
}

public class ImageRepository : IImageRepository
{
    public async Task SaveJsonAsync(string fileName, string jsonContent)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        await File.WriteAllTextAsync(filePath, jsonContent);
    }
}
