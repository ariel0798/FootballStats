using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Prism.Commands;
using Prism.Services;
using Refit;
using Xamarin.Essentials;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        readonly IPageDialogService pageDialogService;

        public TeamViewModel(IPageDialogService pageDialogService)
        {
            this.pageDialogService = pageDialogService;
        }

        public Team Team { get; set; }

        public DelegateCommand GetTeamDataCommand => new DelegateCommand(async () =>
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IRefitFootballApiService>(Config.FootballApiUrl);

                var teams = await serviceApi.GetTeamById(33);

                if (teams != null)
                {
                    this.Team = teams.Api.Teams[0];
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync("Error", "An error occurred connecting to the API", "Ok");
                }
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("No Internet", "Please check your internet connection", "Ok");
            }
        });

    }
}
