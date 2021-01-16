using System.Collections.Generic;

namespace FootballStats.Models.Fixtures
{
    public class Fixtures
    {
        public Api Api { get; set; }
    }

    public class Api
    {
        public int Results { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }
}
