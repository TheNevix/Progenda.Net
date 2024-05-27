using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class BulkUpdateAppointment
    {
        /// <summary>
        /// Progenda identifier for the appointment.
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Your identifier for the appointment.
        /// </summary>
        [JsonProperty("remote_id")]
        public string? RemoteId { get; set; }

        /// <summary>
        /// Your identifier for the patient of the appointment.
        /// </summary>
        [JsonProperty("patient_remote_id")]
        public string? PatientRemoteId { get; set; }

        /// <summary>
        /// The time of the appointment start.
        /// </summary>
        [JsonProperty("start")]
        public int? Start { get; set; }

        /// <summary>
        /// The time of the appointment end.
        /// </summary>
        [JsonProperty("stop")]
        public string? Stop { get; set; }

        /// <summary>
        /// Personal notes about the appointment.
        /// </summary>
        [JsonProperty("notes")]
        public string? Notes { get; set; }

        /// <summary>
        /// Color of the appointment on the user’s interface.
        /// </summary>
        [JsonProperty("color")]
        public string? Color { get; set; }

        /// <summary>
        /// Title of the appointment (e.g. Meeting, Lunch, Break)
        /// </summary>
        [JsonProperty("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Status of the appointment. A “cancelled” appointment cannot become “booked” again. Allows bulk deletion of appointments.
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Your identifier for the appointment type of the appointment.
        /// </summary>
        [JsonProperty("service_remote_id")]
        public string? ServiceRemoteId { get; set; }

        /// <summary>
        /// Time of arrival of the patient for this appointment. If null, the patient has not arrived yet.
        /// </summary>
        [JsonProperty("patient_arrived_at")]
        public string? PatientArrivedAt { get; set; }

        /// <summary>
        /// Set to true if the patient missed his appointment. Defaults to false.
        /// </summary>
        [JsonProperty("noshow")]
        public bool? Noshow { get; set; }
    }
}
