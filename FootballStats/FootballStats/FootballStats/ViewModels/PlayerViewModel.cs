using FootballStats.Models.Players;
using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel, IInitialize
    {
        readonly INavigationService navigationService;
        public PlayerViewModel(IApiManager apiManager, INavigationService navigationService) : base(apiManager)
        {
            Title = "Player";
            NavigateGoBackCommand = new DelegateCommand(async () => await NavigateGoBack());
            this.navigationService = navigationService;
        }


        public void Initialize(INavigationParameters parameters)
        {
            Player = parameters["player"] as Player;
            TeamLogo = parameters["logo"] as string;
            Task.Run(async () => await RunSafe(GetTrophyData()));
        }

        public string Title { get; }
        public string TeamLogo { get; set; }
        public DelegateCommand NavigateGoBackCommand { get; }
        public Player Player { get; set; }
        public List<Trophy> TrophiesList { get; set; }

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
                        TrophiesList = trophies.Api.Trophies;
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

        async Task NavigateGoBack()
        {
            await navigationService.GoBackAsync();
        }
    }
}
