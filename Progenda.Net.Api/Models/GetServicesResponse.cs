using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class GetServicesResponse
    {
        [JsonProperty("services")]
        public List<ServiceDetails> Services { get; set; }
    }

    public class ServiceDetails
    {
        [JsonProperty("service")]
        public Service Serivce { get; set; }
    }

    public class Service
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("remote_id")]
        public string RemoteId { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
