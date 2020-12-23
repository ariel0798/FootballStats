using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FootballStats.ViewModels
{
    class TrophyViewModel : BaseViewModel
    {
        readonly IPageDialogService pageDialogService;

        public TrophyViewModel(IPageDialogService pageDialogService)
        {
            this.pageDialogService = pageDialogService;
        }

        public Trophy Trophy { get; set; }

        public DelegateCommand GetTrophyDataCommand => new DelegateCommand(async () => await RunSafe(GetData()));
        /*
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IFootballApi>(Config.FootballApiUrl);

                var trophy = await serviceApi.GetTrophiesById(276);

                if (trophy != null)
                {
                    this.Trophy = trophy.Api.Trophies[1];
                }
                else
                {
                    await pageDialogService.DisplayAlertAsync("Error", "An error occurred connecting to the API", "Ok");
                }
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("No Internet", "Please check your internet connection", "Ok");
            }
        });
        */
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
