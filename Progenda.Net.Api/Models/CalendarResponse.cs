using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progenda.Net.Api.Models
{
    public class CalendarResponse
    {
        [JsonProperty("calendars")]
        public List<CalendarWrapper> Calendars { get; set; }
    }

    public class CalendarWrapper
    {
        [JsonProperty("calendar")]
        public Calendar Calendar { get; set; }
    }

    public class Calendar
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("center_id")]
        public int CenterId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("specialities")]
        [JsonConverter(typeof(SpecialityConverter))]
        public List<Speciality> Specialities { get; set; }
    }

    public class Speciality
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }

    public class SpecialityConverter : JsonConverter<List<Speciality>>
    {
        public override void WriteJson(JsonWriter writer, List<Speciality> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override List<Speciality> ReadJson(JsonReader reader, Type objectType, List<Speciality> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var specialities = new List<Speciality>();
            var array = JArray.Load(reader);

            foreach (var item in array)
            {
                var speciality = item["speciality"].ToObject<Speciality>();
                specialities.Add(speciality);
            }

            return specialities;
        }
    }
}
