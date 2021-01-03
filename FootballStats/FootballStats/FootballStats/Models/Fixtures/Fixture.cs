using Newtonsoft.Json;
using System;

namespace FootballStats.Models.Fixtures
{
    public class Fixture
    {
        [JsonProperty(propertyName: "fixture_id")] 

        public int FixtureId { get; set; }

        [JsonProperty(propertyName: "league_id")]
        public int LeagueId { get; set; }

        public League League { get; set; }

        [JsonProperty(propertyName: "event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty(propertyName: "event_timestamp")]
        public int EventTimestamp { get; set; }

        public int FirstHalfStart { get; set; }
        public int SecondHalfStart { get; set; }
        public string Round { get; set; }
        public string Status { get; set; }
        public string StatusShort { get; set; }
        public int Elapsed { get; set; }
        public string Venue { get; set; }
        public object Referee { get; set; }
        public HomeTeam SomeTeam { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }
        public Score Score { get; set; }
    }
    public class League
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Flag { get; set; }
    }

    public class HomeTeam
    {
        [JsonProperty(propertyName: "team_id")]
        public int TeamId { get; set; }

        [JsonProperty(propertyName: "team_name")]
        public string TeamName { get; set; }

        public string Logo { get; set; }
    }

    public class AwayTeam
    {
        [JsonProperty(propertyName:"team_id")]
        public int TeamId { get; set; }

        [JsonProperty(propertyName: "team_name")]
        public string TeamName { get; set; }

        public string Logo { get; set; }
    }

    public class Score
    {
        public string Halftime { get; set; }
        public string Fulltime { get; set; }
        public object Extratime { get; set; }
        public object Penalty { get; set; }
    }
}
