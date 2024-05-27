using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class BulkUpdateService
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("remote_id")]
        public string? RemoteId { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("duration")]
        public required int Duration { get; set; }
        [JsonProperty("color")]
        public string? Color { get; set; }
    }
}
