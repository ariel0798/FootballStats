using System.Collections.Generic;

namespace FootballStats.Models.Players
{
    public class Players
    {
        public Api Api { get; set; }
    }
    public class Api
    {
        public int Results { get; set; }
        public List<Player> Players { get; set; }
    }
}
