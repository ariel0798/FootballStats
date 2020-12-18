using FootballStats.Models.Leagues;
using FootballStats.Services.Interfaces;
using Prism.Commands;
using Prism.Services;
using Refit;
using Xamarin.Essentials;


namespace FootballStats.ViewModels
{
   public  class LeagueViewModel : BaseViewModel
    {

        readonly IPageDialogService pageDialogService;

        public LeagueViewModel(IPageDialogService pageDialogService)
        {
            this.pageDialogService = pageDialogService;
        }
        public League League { get; set; }

        public DelegateCommand GetLeagueCommand => new DelegateCommand(async () =>
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IRefitFootballApiService>(Config.FootballApiUrl);

                var leagues = await serviceApi.GetLeaguesById(5);

                if (leagues != null)
                {
                    this.League = leagues.Api.Leagues[0];
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
