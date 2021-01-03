using FootballStats.Models.Leagues;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;


namespace FootballStats.ViewModels
{
   public  class LeagueViewModel : BaseViewModel
    {
        public LeagueViewModel(IApiManager apiManager) : base(apiManager)
        {
        }

        public League League { get; set; }

        public DelegateCommand GetLeagueCommand => new DelegateCommand(async () => await RunSafe(GetData()));

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
