using System.Text.Json.Serialization;

namespace FootballStats.Models
{
    public class Team
    {
        [JsonPropertyName("team_id")]
        public int TeamId { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        public string Country { get; set; }

        [JsonPropertyName("is_national")]
        public bool IsNational { get; set; }

        public int Founded { get; set; }

        [JsonPropertyName("venue_name")]
        public string VenueName { get; set; }

        [JsonPropertyName("venue_surface")]
        public string VenueSurface { get; set; }

        [JsonPropertyName("venue_address")]
        public string VenueAddress { get; set; }

        [JsonPropertyName("venue_city")]
        public string VenueCity { get; set; }

        [JsonPropertyName("venue_capacity")]
        public int VenueCapacity { get; set; }
    }
}
