using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ImageProcessingApp.Tests
{
    public class SimpleIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SimpleIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_HealthCheck_ReturnsSuccess()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/VisionTest/HealthCheck");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("API is working", responseString);
        }

        public async Task Test_Index_UploadImage_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(new byte[0]); // Simulate a file
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Add(fileContent, "imageFile", "test.jpg");

            var response = await client.PostAsync("/VisionTest/Index", content);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Please upload a valid image file.", responseString);
        }
    }
}
