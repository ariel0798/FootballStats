using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FootballStats.Models.Teams
{
    public class Team
    {
        [JsonPropertyName("team_id")]
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        public string Country { get; set; }

        [JsonProperty(propertyName:"is_national")]
        public bool IsNational { get; set; }

        public int Founded { get; set; }

        [JsonProperty(propertyName: "venue_name")]
        public string VenueName { get; set; }

        [JsonProperty(propertyName: "venue_surface")]
        public string VenueSurface { get; set; }

        [JsonProperty(propertyName: "venue_address")]
        public string VenueAddress { get; set; }

        [JsonProperty(propertyName: "venue_city")]
        public string VenueCity { get; set; }

        [JsonProperty(propertyName: "venue_capacity")]
        public int VenueCapacity { get; set; }
    }
}
