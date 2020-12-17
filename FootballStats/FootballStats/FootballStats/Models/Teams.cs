using System.Collections.Generic;

namespace FootballStats.Models
{
    public class Teams
    {
        public Api Api { get; set; }
    }
    public class Api
    {
        public int Results { get; set; }
        public List<Team> Teams { get; set; }
    }
}
