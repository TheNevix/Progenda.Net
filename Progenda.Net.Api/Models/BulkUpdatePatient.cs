using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class BulkUpdatePatient
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("remote_id")]
        public string? RemoteId { get; set; }

#if NET8_0
        [JsonProperty("first_name")]
        public required string FirstName { get; set; }
        [JsonProperty("last_name")]
        public required string LastName { get; set; }
        [JsonProperty("email")]
        public required string Email { get; set; }
        [JsonProperty("phone_number")]
        public required string PhoneNumber { get; set; }
#else
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
#endif
        [JsonProperty("birthdate")]
        public string? Birthdate { get; set; }
        [JsonProperty("address")]
        public string? Address { get; set; }
        [JsonProperty("notes")]
        public string? Notes { get; set; }
        [JsonProperty("language_code")]
        public string? LanguageCode { get; set; }
    }
}
