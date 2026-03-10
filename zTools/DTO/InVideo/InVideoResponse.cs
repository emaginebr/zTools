using Newtonsoft.Json;

namespace zTools.DTO.InVideo
{
    public class InVideoResponse
    {
        [JsonProperty("video_url")]
        public string VideoUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
