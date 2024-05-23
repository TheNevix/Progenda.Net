using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class CreatePatientRequest
    {
        [JsonProperty("remote_id")]
        public required string RemoteId { get; set; }
        [JsonProperty("first_name")]
        public required string FirstName { get; set; }
        [JsonProperty("last_name")]
        public required string LastName { get; set; }
        [JsonProperty("email")]
        public required string Email { get; set; }
        [JsonProperty("phone_number")]
        public required string PhoneNumber { get; set; }
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; } = null;
        [JsonProperty("address")]
        public string Address { get; set; } = null;
        [JsonProperty("notes")]
        public string Notes { get; set; } = null;
        [JsonProperty("language_code")]
        public string LanguageCode { get; set; } = null;
    }
}
