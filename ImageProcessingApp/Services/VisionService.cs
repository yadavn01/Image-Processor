using System.Text.Json;
using Google.Cloud.Vision.V1;

public interface IVisionService
{
    Task<string> ProcessImageAsync(IFormFile imageFile);
}

public class VisionService : IVisionService
{
    private readonly IImageRepository _imageRepository;
    private readonly ImageAnnotatorClient _client;

    public VisionService(IImageRepository imageRepository, ImageAnnotatorClient client)
    {
        _imageRepository = imageRepository;
        _client = client;
    }

    public async Task<string> ProcessImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return "Please upload a valid image file.";
        }

        using var stream = new MemoryStream();
        await imageFile.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);

        var image = Image.FromStream(stream);
        var response = _client.DetectFaces(image);

        int highConfidenceFaceCount = 0;
        bool prepareForModerateConfidenceAction = false;
        bool prepareForLowConfidenceAction = false;

        foreach (var face in response)
        {
            float confidence = face.DetectionConfidence;

            if (confidence >= 0.8f)
            {
                highConfidenceFaceCount++;
            }
            else if (confidence >= 0.7f && confidence < 0.8f)
            {
                prepareForModerateConfidenceAction = true;
                LogModerateConfidenceAction(confidence);
            }
            else
            {
                prepareForLowConfidenceAction = true;
                LogLowConfidenceAction(confidence);
            }
        }

        if (highConfidenceFaceCount > 0)
        {
            var faceData = new
            {
                FaceCount = highConfidenceFaceCount,
                Message = "Faces detected with high confidence."
            };

            string jsonOutput = JsonSerializer.Serialize(faceData);
            await _imageRepository.SaveJsonAsync("faceDetectionResult.json", jsonOutput);

            return $"Detected {highConfidenceFaceCount} face(s) with high confidence.";
        }
        else
        {
            return "No faces detected with high confidence.";
        }
    }

    private void LogModerateConfidenceAction(float confidence)
    {
        Console.WriteLine($"Moderate confidence detected: {confidence * 100}%");
    }

    private void LogLowConfidenceAction(float confidence)
    {
        Console.WriteLine($"Low confidence detected: {confidence * 100}%");
    }
}
