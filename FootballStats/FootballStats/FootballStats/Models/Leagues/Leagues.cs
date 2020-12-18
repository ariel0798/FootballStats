using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStats.Models.Leagues
{
     public class Leagues
    {
        public Api Api { get; set; }
    }
    
    public class Api 
    {

        public int Results { get; set; }
        public List<League> Leagues { get; set; }
    }
}
