

namespace FootballStats.Models.Statistics
{
    public class Statistic
    {
        public Matchs Matchs { get; set; }
        public Goals Goals { get; set; }
        public GoalsAvg GoalsAvg { get; set; }
    }

    public class MatchsPlayed
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Wins
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Draws
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Loses
    {
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }

    public class Matchs
    {
        public MatchsPlayed MatchsPlayed { get; set; }
        public Wins Wins { get; set; }
        public Draws Draws { get; set; }
        public Loses Loses { get; set; }
    }

    public class GoalsFor
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public string Total { get; set; }
    }

    public class GoalsAgainst
    {
        public string Home { get; set; }
        public string Away { get; set; }
        public string Total { get; set; }
    }

    public class Goals
    {
        public GoalsFor GoalsFor { get; set; }
        public GoalsAgainst GoalsAgainst { get; set; }
    }

    public class GoalsAvg
    {
        public GoalsFor GoalsFor { get; set; }
        public GoalsAgainst GoalsAgainst { get; set; }
    }

}
