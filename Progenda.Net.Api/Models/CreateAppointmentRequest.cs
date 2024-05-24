using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class CreateAppointmentRequest
    {
        [JsonProperty("remote_id")]
        public required string RemoteId { get; set; }
        [JsonProperty("patient_remote_id")]
        public string? PatientRemoteId { get; set; } = null;
        [JsonProperty("start")]
        public required string Start { get; set; }
        [JsonProperty("stop")]
        public required string End { get; set; } 
        [JsonProperty("notes")]
        public string? Notes { get; set; } = null;
        [JsonProperty("color")]
        public string? Color { get; set; } = null;
        [JsonProperty("title")]
        public string? Title { get; set; } = null;
        [JsonProperty("status")]
        public string? Status { get; set; } = null;
        [JsonProperty("service_remote_id")]
        public string? ServiceRemoteId { get; set; } = null;
        [JsonProperty("patient_arrived_at")]
        public string? PatientArrivedAt { get; set; } = null;
        [JsonProperty("noshow")]
        public bool? Noshow { get; set; } = null;
    }
}
