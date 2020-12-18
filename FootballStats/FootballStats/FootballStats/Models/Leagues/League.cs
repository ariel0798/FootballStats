using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStats.Models.Leagues
{
    public class FixturesLeague
    {
        public bool Events { get; set; }
        public bool Lineups { get; set; }
        public bool Statistics { get; set; }
        public bool Players_statistics { get; set; }
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

        public int League_id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public object Country_code { get; set; }
        public int Season { get; set; }
        public string Season_start { get; set; }
        public string Season_end { get; set; }
        public string Logo { get; set; }
        public object Flag { get; set; }
        public int Standings { get; set; }
        public int Is_current { get; set; }
        public Coverage Coverage { get; set; }
    }
  

}
