using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using zTools.ACL;
using zTools.DTO.InVideo;
using zTools.DTO.Settings;
using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace zTools.Tests.ACL
{
    public class InVideoClientTests
    {
        private readonly Mock<IOptions<zToolsetting>> _mockSettings;
        private readonly zToolsetting _settings;
        private readonly MockHttpMessageHandler _mockHttpHandler;
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<InVideoClient>> _mockLogger;

        public InVideoClientTests()
        {
            _settings = new zToolsetting
            {
                ApiUrl = "https://api.example.com"
            };

            _mockSettings = new Mock<IOptions<zToolsetting>>();
            _mockSettings.Setup(x => x.Value).Returns(_settings);

            _mockHttpHandler = new MockHttpMessageHandler();
            _httpClient = _mockHttpHandler.ToHttpClient();

            _mockLogger = new Mock<ILogger<InVideoClient>>();
        }

        [Fact]
        public void Constructor_WithValidParameters_CreatesInstance()
        {
            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);
            Assert.NotNull(client);
        }

        [Fact]
        public async Task GenerateVideoAsync_WithPrompt_ReturnsResponse()
        {
            // Arrange
            var expectedResponse = new InVideoResponse
            {
                VideoUrl = "https://invideo.io/videos/test-123",
                Title = "Test Video",
                Description = "A test video"
            };
            var apiUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond("application/json", JsonConvert.SerializeObject(expectedResponse));

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act
            var result = await client.GenerateVideoAsync("Create a test video");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.VideoUrl, result.VideoUrl);
            Assert.Equal(expectedResponse.Title, result.Title);
            Assert.Equal(expectedResponse.Description, result.Description);
        }

        [Fact]
        public async Task GenerateVideoAsync_WithRequest_ReturnsResponse()
        {
            // Arrange
            var request = new InVideoRequest
            {
                Prompt = "Create a video about technology",
                Duration = 5.0,
                Platform = "youtube_shorts",
                Voice = "male",
                Accent = "american"
            };

            var expectedResponse = new InVideoResponse
            {
                VideoUrl = "https://invideo.io/videos/tech-456",
                Title = "Technology Video",
                Description = "A video about technology"
            };
            var apiUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond("application/json", JsonConvert.SerializeObject(expectedResponse));

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act
            var result = await client.GenerateVideoAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.VideoUrl, result.VideoUrl);
            Assert.Equal(expectedResponse.Title, result.Title);
        }

        [Fact]
        public async Task GenerateVideoAsync_MakesCorrectHttpPostRequest()
        {
            // Arrange
            var expectedUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";
            var response = new InVideoResponse
            {
                VideoUrl = "https://invideo.io/videos/test",
                Title = "Test",
                Description = "Test"
            };

            _mockHttpHandler
                .Expect(HttpMethod.Post, expectedUrl)
                .Respond("application/json", JsonConvert.SerializeObject(response));

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act
            await client.GenerateVideoAsync("test prompt");

            // Assert
            _mockHttpHandler.VerifyNoOutstandingExpectation();
        }

        [Fact]
        public async Task GenerateVideoAsync_WhenApiReturns404_ThrowsHttpRequestException()
        {
            // Arrange
            var apiUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond(HttpStatusCode.NotFound);

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() =>
                client.GenerateVideoAsync("test"));
        }

        [Fact]
        public async Task GenerateVideoAsync_WhenApiReturns500_ThrowsHttpRequestException()
        {
            // Arrange
            var apiUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond(HttpStatusCode.InternalServerError);

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() =>
                client.GenerateVideoAsync(new InVideoRequest { Prompt = "test" }));
        }

        [Fact]
        public async Task GenerateVideoAsync_UsesCorrectApiUrl()
        {
            // Arrange
            var customSettings = new zToolsetting { ApiUrl = "https://custom-api.com" };
            var mockCustomSettings = new Mock<IOptions<zToolsetting>>();
            mockCustomSettings.Setup(x => x.Value).Returns(customSettings);

            var expectedResponse = new InVideoResponse
            {
                VideoUrl = "https://invideo.io/videos/custom",
                Title = "Custom",
                Description = "Custom"
            };
            var apiUrl = $"{customSettings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond("application/json", JsonConvert.SerializeObject(expectedResponse));

            var client = new InVideoClient(_httpClient, mockCustomSettings.Object, _mockLogger.Object);

            // Act
            var result = await client.GenerateVideoAsync("test");

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Create a video about cats")]
        [InlineData("Make an ad for shoes")]
        [InlineData("Generate an explainer video")]
        public async Task GenerateVideoAsync_WithVariousPrompts_ReturnsResponse(string prompt)
        {
            // Arrange
            var expectedResponse = new InVideoResponse
            {
                VideoUrl = "https://invideo.io/videos/test",
                Title = "Video",
                Description = "Description"
            };
            var apiUrl = $"{_settings.ApiUrl}/InVideo/generateVideo";

            _mockHttpHandler
                .When(HttpMethod.Post, apiUrl)
                .Respond("application/json", JsonConvert.SerializeObject(expectedResponse));

            var client = new InVideoClient(_httpClient, _mockSettings.Object, _mockLogger.Object);

            // Act
            var result = await client.GenerateVideoAsync(prompt);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.VideoUrl, result.VideoUrl);
        }
    }
}
