using FootballStats.ViewModels;
using FootballStats.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace FootballStats
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer): 
            base(platformInitializer)
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TeamPage,TeamViewModel>(NavigationConstants.TeamPage);
            containerRegistry.RegisterForNavigation<TrophyPage, TrophyViewModel>(NavigationConstants.TrophyPage);
            containerRegistry.RegisterForNavigation<LeaguePage, LeagueViewModel>(NavigationConstants.LeaguePage);
            containerRegistry.RegisterForNavigation<HomePage>(NavigationConstants.HomePage);
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync(NavigationConstants.TeamPage);
        }
    }
}
