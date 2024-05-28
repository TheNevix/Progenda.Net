using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class BulkUpdateService
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("remote_id")]
        public string? RemoteId { get; set; }
#if NET8_0
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("duration")]
        public required int Duration { get; set; }
#else
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
#endif
        [JsonProperty("color")]
        public string? Color { get; set; }
    }
}
