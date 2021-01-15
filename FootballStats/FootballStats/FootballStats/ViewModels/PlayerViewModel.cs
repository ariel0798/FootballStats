using Acr.UserDialogs;
using FootballStats.Constants;
using FootballStats.Models.Players;
using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel, IInitialize
    {
        public string Title { get; }
        public string TeamLogo { get; set; }
        public DelegateCommand NavigateGoBackCommand { get; }
        public Player Player { get; set; }
        public List<Trophy> TrophiesList { get; set; }
        readonly ITrophiesApiManager trophiesApiManager;
        public PlayerViewModel(ITrophiesApiManager trophiesApiManager,
            IUserDialogs userDialogs, INavigationService navigationService)
            : base(userDialogs, navigationService)
        {
            Title = PageTitlesConstants.Player;
            this.trophiesApiManager = trophiesApiManager;
            NavigateGoBackCommand = new DelegateCommand(async () => await NavigateGoBack());
        }

        public void Initialize(INavigationParameters parameters)
        {
            Player = parameters[ParametersConstants.Player] as Player;
            TeamLogo = parameters[ParametersConstants.TeamLogo] as string;
            Task.Run(async () => await RunSafe(GetTrophyData()));
        }

        private async Task NavigateGoBack()
        {
            await navigationService.GoBackAsync();
        }

        private async Task GetTrophyData()
        {
            var trophyFootballResponse = await trophiesApiManager.GetTrophiesByPlayerId(Player.PlayerId);

            if (trophyFootballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await trophyFootballResponse.Content.ReadAsStringAsync();
                var trophies = await Task.Run(() => JsonConvert.DeserializeObject<Trophies>(jsonResponse));
                
                if(trophies != null)
                {
                    if(trophies.Api.Results >= 1)
                    {
                        TrophiesList = trophies.Api.Trophies;
                    }
                    else
                        await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData, 
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
                }
                else
                    await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);

            }
            else
                await pageDialogs.AlertAsync(DialogResponsesConstants.UnableToGetData,
                            DialogResponsesConstants.Error, DialogResponsesConstants.Ok);
        }
    }
}
