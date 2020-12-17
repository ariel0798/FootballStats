using System.Text.Json.Serialization;

namespace FootballStats.Models.Players
{
    public class Player
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public object Number { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; }

        [JsonPropertyName("birth_place")]
        public string BirthPlace { get; set; }

        [JsonPropertyName("birth_country")]
        public string BirthCountry { get; set; }

        public string Nationality { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Injured { get; set; }
        public string Rating { get; set; }

        [JsonPropertyName("team_id")]
        public int TeamId { get; set; }

        [JsonPropertyName("team_name")]

        public string TeamName { get; set; }

        public string League { get; set; }

        public string Season { get; set; }

        public int Captain { get; set; }

        public Shots Shots { get; set; }
        public Goals Goals { get; set; }
        public Passes Passes { get; set; }
        public Tackles Tackles { get; set; }
        public Duels Duels { get; set; }
        public Dribbles Dribbles { get; set; }
        public Fouls Fouls { get; set; }
        public Cards Cards { get; set; }
        public Penalty Penalty { get; set; }
        public Games Games { get; set; }
        public Substitutes Substitutes { get; set; }
    }

    public class Shots
    {
        public int Total { get; set; }
        public int On { get; set; }
    }

    public class Goals
    {
        public int Total { get; set; }
        public int Conceded { get; set; }
        public int Assists { get; set; }
        public int Saves { get; set; }
    }

    public class Passes
    {
        public int Total { get; set; }
        public int Key { get; set; }
        public int Accuracy { get; set; }
    }

    public class Tackles
    {
        public int Total { get; set; }
        public int Blocks { get; set; }
        public int Interceptions { get; set; }
    }

    public class Duels
    {
        public int Total { get; set; }
        public int Won { get; set; }
    }

    public class Dribbles
    {
        public int Attempts { get; set; }
        public int Success { get; set; }
    }

    public class Fouls
    {
        public int Drawn { get; set; }
        public int Committed { get; set; }
    }

    public class Cards
    {
        public int Yellow { get; set; }
        public int Yellowred { get; set; }
        public int Red { get; set; }
    }

    public class Penalty
    {
        public int Won { get; set; }
        public int Commited { get; set; }
        public int Success { get; set; }
        public int Missed { get; set; }
        public int Saved { get; set; }
    }

    public class Games
    {
        public int Appearences { get; set; }
        [JsonPropertyName("minutes_played")]
        public int MinutesPlayed { get; set; }
        public int Lineups { get; set; }
    }

    public class Substitutes
    {
        public int @In { get; set; }
        public int @Out { get; set; }
        public int Bench { get; set; }
    }

}
