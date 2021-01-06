using Acr.UserDialogs;
using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public TeamViewModel( IApiManager apiManager)
            :base(apiManager)
        {
            Task.Run(async () => await RunSafe(GetData()));
        }

        public List <Team> TeamList { get; set; }
        
      
        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTeamByLeagueId(2);

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var teams = await Task.Run(() => JsonConvert.DeserializeObject<Teams>(jsonResponse));
                var teamsContent = teams;
                TeamList = teams.Api.Teams;
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
