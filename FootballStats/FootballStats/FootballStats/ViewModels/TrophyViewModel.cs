using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services;
using System.Threading.Tasks;
namespace FootballStats.ViewModels
{
    class TrophyViewModel : BaseViewModel
    {
        public TrophyViewModel(IApiManager apiManager) : base(apiManager)
        {
        }

        public Trophy Trophy { get; set; }

        public DelegateCommand GetTrophyDataCommand => new DelegateCommand(async () => await RunSafe(GetData()));
        
        async Task GetData()
        {
            var footballResponse = await ApiManager.GetTrophiesById(276);

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var trophies = await Task.Run(() => JsonConvert.DeserializeObject<Trophies>(jsonResponse));
                Trophy = trophies.Api.Trophies[1];
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }
    }
}
