using FootballStats.Models.Players;
using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel, IInitialize
    {
        
        public PlayerViewModel(IApiManager apiManager) : base(apiManager)
        {
            
        }

        public void Initialize(INavigationParameters parameters)
        {

            Player = parameters["player"] as Player;
            //Task.Run(async () => await RunSafe(GetTrophyData()));
        }
        public Player Player { get; set; }
        public Trophy Trophy { get; set; }

        async Task GetTrophyData()
        {
            
            var footballResponse = await ApiManager.GetTrophiesByPlayerId(Player.PlayerId);

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var trophies = await Task.Run(() => JsonConvert.DeserializeObject<Trophies>(jsonResponse));
                
                if(trophies != null)
                {
                    if(trophies.Api.Results >= 1)
                    {
                        Trophy = trophies.Api.Trophies[1];
                    }
                    else
                        await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
                }
                else
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");

            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
        }
    }
}
