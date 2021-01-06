using FootballStats.Services;
using FootballStats.Services.Interfaces;
using FootballStats.ViewModels;
using FootballStats.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;

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
            containerRegistry.RegisterForNavigation<HomePage,HomePageViewModel>(NavigationConstants.HomePage);


            containerRegistry.RegisterInstance<IApiManager>(new ApiManager(new ApiService<IFootballApi>(Config.FootballApiUrl)));
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync(new Uri($"/{NavigationConstants.TeamPage}"));
        }
    }
}
