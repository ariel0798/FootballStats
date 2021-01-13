using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prism.Navigation;
using FootballStats.Models.Leagues;
using System.Linq;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        readonly INavigationService navigationService;
        public TeamViewModel( IApiManager apiManager, INavigationService navigationService)
            :base(apiManager)
        {
            Title = "Teams";

            this.navigationService = navigationService;

            NavigateToTeamStatCommand = new DelegateCommand<Team>( async (team) => await NavigateToTeamStat(team));

            Task.Run(async () => await RunSafe(GetData()));
            
        }

        public string Title { get; }

        public List<Team> TeamList { get; set; }
        public List<League> Leagues { get; set; }
       
        public DelegateCommand<Team> NavigateToTeamStatCommand { get; }
        private int leagueId = 2;

        async Task NavigateToTeamStat(Team team)
        {
            var parameters = new NavigationParameters
            {
                { "teamId", team.TeamId },
                { "name", team.Name },
                { "logo", team.Logo },
                { "leagueId", leagueId }
            };
            await navigationService.NavigateAsync(NavigationConstants.TeamStatsPage,parameters);
        }


        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTeamByLeagueId(leagueId);
            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var teams = await Task.Run(() => JsonConvert.DeserializeObject<Teams>(jsonResponse));
                TeamList = teams.Api.Teams;
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
