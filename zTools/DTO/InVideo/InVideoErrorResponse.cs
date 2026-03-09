using Newtonsoft.Json;

namespace zTools.DTO.InVideo
{
    public class InVideoErrorResponse
    {
        [JsonProperty("error")]
        public InVideoError Error { get; set; }
    }

    public class InVideoError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
