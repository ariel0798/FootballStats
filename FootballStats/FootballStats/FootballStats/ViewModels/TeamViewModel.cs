using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services;
using Refit;
using System.Threading.Tasks;
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
        //await RunSafe(GetData()));
        public DelegateCommand GetTeamDataCommand => new DelegateCommand(async () =>
        
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IFootballApi>(Config.FootballApiUrl);

                var teams = await serviceApi.GetTeamById(33);

                if (teams != null)
                {
                    var stringJson = teams.Content.ReadAsStringAsync().ToString();
                    var teamObj = JsonConvert.DeserializeObject<Teams>(stringJson);
                    this.Team = teamObj.Api.Teams[0];
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
        /*
        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTeamById(33);

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var teams = await Task.Run(() => JsonConvert.DeserializeObject<Teams>(jsonResponse));
                Team = teams.Api.Teams[0];
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }*/
    }
}
