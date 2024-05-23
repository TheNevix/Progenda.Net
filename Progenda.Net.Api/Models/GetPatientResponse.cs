using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class GetPatientResponse
    {
        [JsonProperty("patient")]
        public Patient Patient { get; set; }
    }
}
