using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class UpdateAppointmentRequest
    {
        [JsonProperty("patient_remote_id")]
        public string? PatientRemoteId { get; set; }
        [JsonProperty("start")]
        public string? Start { get; set; }
        [JsonProperty("stop")]
        public string? End { get; set; }
        [JsonProperty("notes")]
        public string? Notes { get; set; }
        [JsonProperty("color")]
        public string? Color { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Can only be "booked", "cancelled" or "noshow".
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("service_remote_id")]
        public string? ServiceRemoteId { get; set; }
        [JsonProperty("patient_arrived_at")]
        public string? PatientArrivedAt { get; set; }
        [JsonProperty("noshow")]
        public bool? NoShow { get; set; }
    }
}
