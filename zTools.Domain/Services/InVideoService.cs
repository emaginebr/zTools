using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using zTools.Domain.Services.Interfaces;
using zTools.DTO.InVideo;
using zTools.DTO.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace zTools.Domain.Services
{
    public class InVideoService : IInVideoService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<InVideoSetting> _inVideoSettings;

        public InVideoService(HttpClient httpClient, IOptions<InVideoSetting> inVideoSettings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _inVideoSettings = inVideoSettings ?? throw new ArgumentNullException(nameof(inVideoSettings));
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
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _inVideoSettings.Value.ApiKey);

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(
                _inVideoSettings.Value.ApiUrl,
                jsonContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<InVideoErrorResponse>(responseContent);

                if (errorResponse?.Error != null && !string.IsNullOrEmpty(errorResponse.Error.Message))
                {
                    throw new InvalidOperationException(errorResponse.Error.Message);
                }

                throw new InvalidOperationException("Unknown error");
            }

            return JsonConvert.DeserializeObject<InVideoResponse>(responseContent);
        }
    }
}
