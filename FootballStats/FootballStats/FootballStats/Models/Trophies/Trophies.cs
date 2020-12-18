using System.Collections.Generic;

namespace FootballStats.Models.Trophies
{
    public class Trophies
    {
        public Api Api { get; set; }
    }

    public class Api
    {       
        public int Results { get; set; }
        public List<Trophy> Trophies { get; set; }
    }
}
