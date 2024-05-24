using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class GetAppointmentResponse
    {
        [JsonProperty("appointment")]
        public Appointment Appointment { get; set; }
    }
}
