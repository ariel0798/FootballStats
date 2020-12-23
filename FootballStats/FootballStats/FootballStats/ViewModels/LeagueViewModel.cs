using FootballStats.Models.Leagues;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services;
using Refit;
using System.Threading.Tasks;
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

        public DelegateCommand GetLeagueCommand => new DelegateCommand(async () => await RunSafe(GetData()));
        /*{
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IFootballApi>(Config.FootballApiUrl);

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
            
        });*/
        async Task GetData()
        {
            var footballResponse = await ApiManager.GetLeaguesById(5);

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var leagues = await Task.Run(() => JsonConvert.DeserializeObject<Leagues>(jsonResponse));
                League = leagues.Api.Leagues[0];
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
}
        }
    }
}
