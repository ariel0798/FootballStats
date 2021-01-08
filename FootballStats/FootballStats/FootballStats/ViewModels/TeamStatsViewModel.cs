using FootballStats.Models.Players;
using FootballStats.Models.Statistics;
using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class TeamStatsViewModel : BaseViewModel
    {
        public TeamStatsViewModel(IApiManager apiManager) : base(apiManager)
        {
            Task.Run(async () => await RunSafe(GetTeamData()));
        }

        public Statistic Statistic { get; set; }
        public Team Team { get; set; }
        public List<TeamStats> TeamStatsList { get; set; }
        public List<Player> PlayersList { get; set; }
        private int teamId = 33;

        async Task GetTeamData() 
        {
            var footballTeamResponse = await ApiManager.GetTeamByTeamId(teamId);

            if (footballTeamResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballTeamResponse.Content.ReadAsStringAsync();
                var teams = await Task.Run(() => JsonConvert.DeserializeObject<Teams>(jsonResponse));

                if (teams != null)
                {
                    if (teams.Api.Teams.Count >= 1)
                    {
                        Team = teams.Api.Teams[0];
                    }
                    else
                        await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
                }
                else
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");

            await RunSafe(GetStatistic());
        }

        async Task GetStatistic()
        {
            var footballStatsResponse = await ApiManager.GetTeamStatisticsByLeagueIdAndTeamId(2, teamId);
            

            if (footballStatsResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballStatsResponse.Content.ReadAsStringAsync();
                var statistics = await Task.Run(() => JsonConvert.DeserializeObject<Statistics>(jsonResponse));

                if (statistics != null)
                {
                    if(statistics.Api.Results == 1)
                    {
                        Statistic = statistics.Api.Statistics;
                        SetTeamStats(statistics.Api.Statistics);
                    }
                    else
                        await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
                }
                else
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            await RunSafe(GetPlayers());
        }

        async Task GetPlayers()
        {
            
            var footballStatsResponse = await ApiManager.GetPlayersStatsByTeamId(teamId);


            if (footballStatsResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballStatsResponse.Content.ReadAsStringAsync();
                var players = await Task.Run(() => JsonConvert.DeserializeObject<Players>(jsonResponse));

                if (players != null)
                {
                    if (players.Api.Results >= 1)
                    {
                        PlayersList = players.Api.Players;
                    }
                    else
                        await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
                }
                else
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
        }


        private void SetTeamStats(Statistic statistic) 
        { 
            TeamStatsList = new List<TeamStats>();
            var teamStats = new TeamStats();

            teamStats.Name = "Wins";
            teamStats.Home = statistic.Matchs.Wins.Home;
            teamStats.Away = statistic.Matchs.Wins.Away;
            teamStats.Total = statistic.Matchs.Wins.Total;

            TeamStatsList.Add(teamStats);

            var teamStats2 = new TeamStats();


            teamStats2.Name = "Loses";
            teamStats2.Home = statistic.Matchs.Loses.Home;
            teamStats2.Away = statistic.Matchs.Loses.Away;
            teamStats2.Total = statistic.Matchs.Loses.Total;

            TeamStatsList.Add(teamStats2);

            var teamStats3 = new TeamStats();

            teamStats3.Name = "Draws";
            teamStats3.Home = statistic.Matchs.Draws.Home;
            teamStats3.Away = statistic.Matchs.Draws.Away;
            teamStats3.Total = statistic.Matchs.Draws.Total;

            TeamStatsList.Add(teamStats3);

            var teamStats4 = new TeamStats();

            teamStats4.Name = "Matchs played";
            teamStats4.Home = statistic.Matchs.MatchsPlayed.Home;
            teamStats4.Away = statistic.Matchs.MatchsPlayed.Away;
            teamStats4.Total = statistic.Matchs.MatchsPlayed.Total;

            TeamStatsList.Add(teamStats4);
        }
    }

}
