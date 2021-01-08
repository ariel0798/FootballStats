using FootballStats.Models.Players;
using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(IApiManager apiManager) : base(apiManager)
        {
            Task.Run(async () => await RunSafe(GetPlayerData()));
        }

        public Player Player { get; set; }
        public Trophy Trophy { get; set; }

        async Task GetPlayerData()
        {
            int playerId = 33;
            var footballTeamResponse = await ApiManager.GetPlayerStatsByPlayerId(playerId);

            if (footballTeamResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballTeamResponse.Content.ReadAsStringAsync();
                var players = await Task.Run(() => JsonConvert.DeserializeObject<Players>(jsonResponse));

                if (players != null)
                {
                    if (players.Api.Players.Count >= 1)
                    {
                        Player = players.Api.Players[0];
                    }
                    else
                        await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
                }
                else
                    await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
            else
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");

           // await RunSafe(GetTrophyData());
        }

        async Task GetTrophyData()

        {
            int playerId = 33;
            var footballResponse = await ApiManager.GetTrophiesByPlayerId(playerId);

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
