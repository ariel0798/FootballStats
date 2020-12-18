using FootballStats.Models.Leagues;
using FootballStats.Services.Interfaces;
using Prism.Commands;
using Prism.Services;
using Refit;
using Xamarin.Essentials;


namespace FootballStats.ViewModels
{
   public  class LeagueViewModel : BaseViewModel
    {

        readonly IPageDialogService pageDialogService;

        public LeagueViewModel(IPageDialogService pageDialogService)
        {
            this.pageDialogService = pageDialogService;
        }
        public League League { get; set; }

        public DelegateCommand GetLeagueCommand =>
    }
}
