using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class PatientResponse
    {
        [JsonProperty("patients")]
        public List<PatientDetails> PatientDetails { get; set; }
    }

    public class PatientDetails
    {
        [JsonProperty("patient")]
        public Patient Patient { get; set; }
    }

    public class Patient
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("remote_id")]
        public int? RemoteId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("notes")]
        public string Notes { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("language_code")]
        public string LanguageCode { get; set; }
    }
}
