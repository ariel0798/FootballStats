using Acr.UserDialogs;
using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public TeamViewModel( IApiManager apiManager)
            :base(apiManager)
        {
          
        }

        public Team Team { get; set; }
        
        public DelegateCommand GetTeamDataCommand => new DelegateCommand(async () => await RunSafe(GetData()));


        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTeamByTeamId(33);

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
        }
    }
}
