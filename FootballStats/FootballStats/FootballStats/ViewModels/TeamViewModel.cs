using FootballStats.Models.Teams;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prism.Navigation;
using FootballStats.Models.Leagues;
using FootballStats.Constants;
using Acr.UserDialogs;
using FootballStats.Services.Interfaces;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public string Title { get; }
        public List<Team> TeamList { get; set; }
        public List<League> Leagues { get; set; }
        public DelegateCommand<Team> NavigateToTeamStatCommand { get; }

        readonly int leagueId;

        public TeamViewModel(IApiManager apiManager,
            IUserDialogs userDialogs, INavigationService navigationService)
            : base(apiManager,userDialogs, navigationService)
        {
            Title = PageTitlesConstants.Teams;
            leagueId = 2;
            NavigateToTeamStatCommand = new DelegateCommand<Team>( async (team) => await NavigateToTeamStat(team));
            Task.Run(async () => await RunSafe(GetTeamData()));
            
        }

        private async Task NavigateToTeamStat(Team team)
        {
            var parameters = new NavigationParameters
            {
                { ParametersConstants.Team, team },
                { ParametersConstants.LeagueId, leagueId }
            };
            await navigationService.NavigateAsync(NavigationConstants.TeamStatsPage,parameters);
        }

        private async Task GetTeamData()
        {
            var footballResponse = await apiManager.GetTeamByLeagueId(leagueId);
            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var teams = await Task.Run(() => JsonConvert.DeserializeObject<Teams>(jsonResponse));
                TeamList = teams.Api.Teams;
            }
            else
            {
                await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
            }
        }
    }
}
