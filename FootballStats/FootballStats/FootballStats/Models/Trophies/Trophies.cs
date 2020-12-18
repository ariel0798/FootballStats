using System.Collections.Generic;

namespace FootballStats.Models.Trophies
{
    public class Trophies
    {
        public Api api { get; set; }
    }

    public class Api
    {       
        public int Results { get; set; }
        public List<Trophy> Trophies { get; set; }
    }
}
