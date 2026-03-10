using Newtonsoft.Json;

namespace zTools.DTO.InVideo
{
    public class InVideoRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("duration")]
        public double? Duration { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("voice")]
        public string Voice { get; set; }

        [JsonProperty("accent")]
        public string Accent { get; set; }
    }
}
