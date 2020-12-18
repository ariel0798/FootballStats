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

        public int results { get; set; }
        public List<League> leagues { get; set; }
    }
}
