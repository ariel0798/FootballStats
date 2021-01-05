using Acr.UserDialogs;
using FootballStats.Models.Fixtures;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(IApiManager apiManager) : base(apiManager)
        {
        }

        public Fixture Fixture {get;set;}

        public DelegateCommand GetFixtureDataCommand => new DelegateCommand(async () => await RunSafe(GetData()));


        async Task GetData() 
        {
            var footballResponse = await ApiManager.GetFixturesLive();

            if (footballResponse.IsSuccessStatusCode) 
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var fixtures = await Task.Run(() => JsonConvert.DeserializeObject<Fixtures>(jsonResponse));
                Fixture = fixtures.Api.Fixtures[0];
            }
            else 
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
        

    }
}
