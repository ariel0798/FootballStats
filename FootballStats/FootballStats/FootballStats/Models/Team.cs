using Newtonsoft.Json;

namespace FootballStats.Models
{
    public class Team
    {
        [JsonProperty("team_id")]
        public int TeamId { get; set; }
        
        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        public string Country { get; set; }

        [JsonProperty("is_national")]
        public bool IsNational { get; set; }

        public int Founded { get; set; }

        [JsonProperty("venue_name")]
        public string VenueName { get; set; }

        [JsonProperty("venue_surface")]
        public string VenueSurface { get; set; }

        [JsonProperty("venue_address")]
        public string VenueAddress { get; set; }

        [JsonProperty("venue_city")]
        public string VenueCity { get; set; }

        [JsonProperty("venue_capacity")]
        public int VenueCapacity { get; set; }
    }
}
