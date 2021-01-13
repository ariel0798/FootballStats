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
        readonly INavigationService navigationService;
        public TeamStatsViewModel(IApiManager apiManager, INavigationService navigationService) : base(apiManager)
        {
            Title = "Team Statistic";
            this.navigationService = navigationService;
            NavigateToPlayerCommand = new DelegateCommand<Player>(async (player) => await NavigateToPlayer(player));
            NavigateGoBackCommand = new DelegateCommand(async () => await NavigateGoBack());
        }
        public DelegateCommand<Player> NavigateToPlayerCommand { get; }
        public DelegateCommand NavigateGoBackCommand { get; }
        public void Initialize(INavigationParameters parameters)
        {
            var teamId = parameters["teamId"];
            var name = parameters["name"];
            var logo = parameters["logo"];

            leagueId = Convert.ToInt32(parameters["leagueId"]);

            Team = new Team()
            {
                TeamId = Convert.ToInt32(teamId),
                Name = name.ToString(),
                Logo = logo.ToString()
            };

            Task.Run(async () => await RunSafe(GetStatistic()));
        }


        private int leagueId;
        public string Title { get; }

        public Statistic Statistic { get; set; }
        public Team Team { get; set; }
        public List<TeamStats> TeamStatsList { get; set; }
        public List<Player> PlayersList { get; set; }
        public bool NotExistingStats { get; set; }

        async Task GetStatistic()
        {
            var footballStatsResponse = await ApiManager.GetTeamStatisticsByLeagueIdAndTeamId(leagueId, Team.TeamId);
            

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
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            await RunSafe(GetPlayers());
        }

        async Task GetPlayers()
        {
            
            var footballStatsResponse = await ApiManager.GetPlayersStatsByTeamId(Team.TeamId);


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
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
        }
        async Task NavigateToPlayer(Player player)
        {
            var parameters = new NavigationParameters
            {
                { "player", player },
                { "logo", Team.Logo }
            };
            await navigationService.NavigateAsync(NavigationConstants.PlayerPage, parameters);
        }

        async Task NavigateGoBack()
        {
            await navigationService.GoBackAsync();
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

        void SetMockPhotos(List<Player> players)
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

        List<string> GetShufflePhotoList()
        {
            var photos = new List<string>()
            {
                /*
                "https://marriedbiography.com/wp-content/uploads/2018/05/Paulo-Dybala.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2021/324x324/250017925.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2021/324x324/95803.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2021/324x324/1900730.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2021/324x324/250054829.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2021/324x324/250039900.jpg",
                "https://pm1.narvii.com/6120/6c222bfe98b6d8dca27fe28fada6465ae1529db6_00.jpg",
                "https://www.bayernforum.com/threadlogos/11921.jpg",
                "https://www.soccerpunter.com/images/gsm/players/150x150/53.png",
                "https://gsports.live/football/wp-content/uploads/sites/2/2016/08/Robert-Lewandowski-300x300.jpg",
                "https://2.bp.blogspot.com/-uYvypTv2zIk/VKMaUozSKdI/AAAAAAAANzw/mxzRuUbcsmw/s1600/Mesut%2B%C3%96zil.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/250015966.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/59142.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/74055.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/74327.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/250015808.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/250024456.jpg",
                "https://img.uefa.com/imgml/TP/players/1/2016/324x324/99350.jpg"
                */

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

        List<string> Shuffle(List<string> list)
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
