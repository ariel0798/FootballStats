
namespace FootballStats.Models.Statistics
{
    public class Statistics
    {
        public Api Api { get; set; }
    }

    public class Api
    {
        public int Results { get; set; }
        public Statistic Statistics { get; set; }
    }

}
