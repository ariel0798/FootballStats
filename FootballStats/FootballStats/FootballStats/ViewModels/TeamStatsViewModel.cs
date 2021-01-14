using Acr.UserDialogs;
using FootballStats.Constants;
using FootballStats.Models.Players;
using FootballStats.Models.Statistics;
using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class TeamStatsViewModel : BaseViewModel, IInitialize
    {
        public string Title { get; }
        public Statistic Statistic { get; private set; }
        public Team Team { get; private set; }
        public List<TeamStats> TeamStatsList { get; private set; }
        public List<Player> PlayersList { get; private set; }
        public bool NotExistingStats { get; private set; }
        public DelegateCommand<Player> NavigateToPlayerCommand { get; }
        public DelegateCommand NavigateGoBackCommand { get; }

        private int leagueId;

        public TeamStatsViewModel(IApiManager apiManager,
            IUserDialogs userDialogs, INavigationService navigationService)
            : base(apiManager, userDialogs, navigationService)
        {
            Title = PageTitlesConstants.TeamStatistic;
            NavigateToPlayerCommand = new DelegateCommand<Player>(async (player) => await NavigateToPlayer(player));
            NavigateGoBackCommand = new DelegateCommand(async () => await NavigateGoBack());
        }
        
        public void Initialize(INavigationParameters parameters)
        {
            Team = parameters[ParametersConstants.Team] as Team;
            leagueId = Convert.ToInt32(parameters[ParametersConstants.LeagueId]);
            Task.Run(async () => await RunSafe(GetStatisticData()));
        }

        private async Task NavigateToPlayer(Player player)
        {
            var parameters = new NavigationParameters
            {
                { ParametersConstants.Player, player },
                { ParametersConstants.TeamLogo, Team.Logo }
            };
            await navigationService.NavigateAsync(NavigationConstants.PlayerPage, parameters);
        }

        private async Task NavigateGoBack()
        {
            await navigationService.GoBackAsync();
        }

        private async Task GetStatisticData()
        {
            var footballStatsResponse = await apiManager.GetTeamStatisticsByLeagueIdAndTeamId(leagueId, Team.TeamId);

            if (footballStatsResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballStatsResponse.Content.ReadAsStringAsync();
                var statistics = await Task.Run(() => JsonConvert.DeserializeObject<Statistics>(jsonResponse));

                if (statistics != null)
                {
                    if (statistics.Api.Results == 1)
                    {
                        Statistic = statistics.Api.Statistics;
                        SetTeamStats(statistics.Api.Statistics);
                        NotExistingStats = false;
                    }
                    else
                        NotExistingStats = true;
                }
                else
                    await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
            }
            else
                await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);

            await RunSafe(GetPlayersData());
        }

        private async Task GetPlayersData()
        {
            var footballStatsResponse = await apiManager.GetPlayersStatsByTeamId(Team.TeamId);

            if (footballStatsResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballStatsResponse.Content.ReadAsStringAsync();
                var players = await Task.Run(() => JsonConvert.DeserializeObject<Players>(jsonResponse));

                if (players != null)
                {
                    PlayersList = players.Api.Players;

                    if (players.Api.Results >= 1)
                        SetMockPhotos(PlayersList);

                }
                else
                    await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
            }
            else
                await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
        }

        private void SetTeamStats(Statistic statistic) 
        { 
            TeamStatsList = new List<TeamStats>();
            var teamStats = new TeamStats
            {
                Name = "Wins",
                Home = statistic.Matchs.Wins.Home,
                Away = statistic.Matchs.Wins.Away,
                Total = statistic.Matchs.Wins.Total
            };

            TeamStatsList.Add(teamStats);

            var teamStats2 = new TeamStats
            {
                Name = "Loses",
                Home = statistic.Matchs.Loses.Home,
                Away = statistic.Matchs.Loses.Away,
                Total = statistic.Matchs.Loses.Total
            };

            TeamStatsList.Add(teamStats2);

            var teamStats3 = new TeamStats
            {
                Name = "Draws",
                Home = statistic.Matchs.Draws.Home,
                Away = statistic.Matchs.Draws.Away,
                Total = statistic.Matchs.Draws.Total
            };

            TeamStatsList.Add(teamStats3);

            var teamStats4 = new TeamStats
            {
                Name = "Matchs played",
                Home = statistic.Matchs.MatchsPlayed.Home,
                Away = statistic.Matchs.MatchsPlayed.Away,
                Total = statistic.Matchs.MatchsPlayed.Total
            };

            TeamStatsList.Add(teamStats4);
        }

        private void SetMockPhotos(List<Player> players)
        {
            List<string> photos = GetShufflePhotoList();
            int playersNumber = players.Count;
            int photosNumber = photos.Count;

            if(playersNumber <= photosNumber)
            {
                for (int i = 0; i < playersNumber; i++)
                {
                    players[i].Photo = photos[i];
                }
            }
            else
            {
                int counter = 0;
                for (int i = 0; i < playersNumber; i++)
                {
                    players[i].Photo = photos[counter];
                    counter++;

                    if (counter >= photosNumber)
                        counter = 0;
                }
            }
        }

        private List<string> GetShufflePhotoList()
        {
            var photos = new List<string>()
            {
                "Player1.PNG",
                "Player2.PNG",
                "Player3.PNG",
                "Player4.PNG",
                "Player5.PNG",
                "Player6.PNG",
                "Player7.PNG",
                "Player8.PNG",
                "Player9.PNG",
                "Player10.PNG",
                "Player11.PNG",
                "Player12.PNG",
                "Player13.PNG",
                "Player14.PNG",
                "Player15.PNG",
                "Player16.PNG",
                "Player17.PNG",
                "Player18.PNG"
            };

            return Shuffle(photos);
        }

        private List<string> Shuffle(List<string> list)
        {
            int n = list.Count;
            Random rng = new Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }       
    }
}
