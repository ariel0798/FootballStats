﻿using Newtonsoft.Json;

namespace FootballStats.Models.Leagues
{
    public class FixturesLeague
    {
        public bool Events { get; set; }
        public bool Lineups { get; set; }
        public bool Statistics { get; set; }
        [JsonProperty(propertyName: "players_statistics")]

        public bool PlayersStatistics { get; set; }
    }

    public class Coverage
    {
        public bool Standings { get; set; }
        public FixturesLeague Fixtures { get; set; }
        public bool Players { get; set; }
        public bool TopScorers { get; set; }
        public bool Predictions { get; set; }
        public bool Odds { get; set; }
    }
    public class League
    {
        [JsonProperty(propertyName: "league_id")]
        public int LeagueId { get; set; }
      
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        [JsonProperty(propertyName: "country_code")]
        public object CountryCode { get; set; }
        public int Season { get; set; }
        [JsonProperty(propertyName: "season_start")]
        public string SeasonStart { get; set; }
        [JsonProperty(propertyName: "season_end")]
        public string SeasonEnd { get; set; }
        public string Logo { get; set; }
        public object Flag { get; set; }
        public int Standings { get; set; }
        [JsonProperty(propertyName: " Is_current ")]
        public int IsCurrent { get; set; }
        public Coverage Coverage { get; set; }
    }
  

}
