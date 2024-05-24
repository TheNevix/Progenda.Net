using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class GetAppointmentsResponse
    {
        [JsonProperty("appointments")]
        public List<AppointmentDetails> Appointments { get; set; }
    }

    public class AppointmentDetails
    {
        [JsonProperty("appointment")]
        public Appointment Appointment { get; set; }
    }

    public class Appointment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("remote_id")]
        public string RemoteId { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("stop")]
        public long Stop { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("warning")]
        public bool Warning { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("patient_id")]
        public int? PatientId { get; set; } = null;

        [JsonProperty("patient_remote_id")]
        public string PatientRemoteId { get; set; }

        [JsonProperty("service_remote_id")]
        public string ServiceRemoteId { get; set; }

        [JsonProperty("noshow")]
        public bool NoShow { get; set; }

        [JsonProperty("patient_arrived_at")]
        public string PatientArrivedAt { get; set; }
    }

}
