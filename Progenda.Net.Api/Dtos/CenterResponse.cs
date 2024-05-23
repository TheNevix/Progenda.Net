using Newtonsoft.Json;

namespace Progenda.Net.Api.Dtos
{
    public class CenterResponse
    {
        [JsonProperty("centers")]
        public List<CenterDetails> CenterDetails { get; set; }
    }

    public class CenterDetails
    {
        [JsonProperty("center")]
        public Center Center { get; set; }
    }

    public class Center
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
