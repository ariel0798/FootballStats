using FootballStats.Models.Trophies;
using FootballStats.Services.Interfaces;
using Prism.Commands;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
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

        public DelegateCommand GetTrophyDataCommand => new DelegateCommand(async () =>
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var serviceApi = RestService.For<IRefitFootballApiService>(Config.FootballApiUrl);

                var trophy = await serviceApi.GetTrophiesById(276);

                if (trophy != null)
                {
                    this.Trophy = trophy.api.Trophies[1];
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
    }
}
