using Newtonsoft.Json;

namespace Progenda.Net.Api.Models
{
    public class GetSuggestionsResponse
    {
        [JsonProperty("suggestions")]
        public List<SuggestionDetails> Suggestions { get; set; }
    }
    
    public class SuggestionDetails
    {
        [JsonProperty("suggestion")]
        public Suggestion Suggestion { get; set; }
    }

    public class Suggestion
    {
        [JsonProperty("calendar_id")]
        public int CalendarId { get; set; }
        [JsonProperty("service_id")]
        public int ServiceId { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("stop")]
        public DateTime Stop { get; set; }
    }
}
