using Acr.UserDialogs;
using FootballStats.Models.Teams;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace FootballStats.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public TeamViewModel( IApiManager apiManager)
            :base(apiManager)
        {
            Task.Run(async () => await RunSafe(GetData()));
        }

        public List<Team> TeamList { get; set; }

        public DelegateCommand PruebaCommand => new DelegateCommand(() => Prueba());
        public DelegateCommand<Team> GoToTeamDetailCommand => new DelegateCommand<Team>((team) =>
            GoToTeamDetail(team));

       

        public void GoToTeamDetail(Team team)
        {
            int go = 5;
            string letsGo = "";
        }


        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTeamByLeagueId(2);

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
