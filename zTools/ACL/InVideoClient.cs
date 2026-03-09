using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using zTools.ACL.Interfaces;
using zTools.DTO.InVideo;
using zTools.DTO.Settings;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace zTools.ACL
{
    public class InVideoClient : IInVideoClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<zToolsetting> _zToolsetting;
        private readonly ILogger<InVideoClient> _logger;

        public InVideoClient(HttpClient httpClient, IOptions<zToolsetting> zToolsetting, ILogger<InVideoClient> logger)
        {
            _httpClient = httpClient;
            _zToolsetting = zToolsetting;
            _logger = logger;
        }

        public async Task<InVideoResponse> GenerateVideoAsync(string prompt)
        {
            var request = new InVideoRequest
            {
                Prompt = prompt
            };

            return await GenerateVideoAsync(request);
        }

        public async Task<InVideoResponse> GenerateVideoAsync(InVideoRequest request)
        {
            var url = $"{_zToolsetting.Value.ApiUrl}/InVideo/generateVideo";
            _logger.LogInformation("Generating video with InVideo AI via URL: {Url}", url);

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("InVideo AI video generation response received");

            return JsonConvert.DeserializeObject<InVideoResponse>(json);
        }
    }
}
