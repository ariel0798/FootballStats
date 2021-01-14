using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using FootballStats.Views;
using FootballStats.Services;
using FootballStats.Constants;
using FootballStats.ViewModels;
using FootballStats.Services.Interfaces;

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
            containerRegistry.RegisterForNavigation<NavigationPage>(NavigationConstants.NagivationPage);
            containerRegistry.RegisterForNavigation<TeamPage,TeamViewModel>(NavigationConstants.TeamPage);
            containerRegistry.RegisterForNavigation<HomePage,HomeViewModel>(NavigationConstants.HomePage);
            containerRegistry.RegisterForNavigation<TeamStatsPage,TeamStatsViewModel>(NavigationConstants.TeamStatsPage);
            containerRegistry.RegisterForNavigation<PlayerPage,PlayerViewModel>(NavigationConstants.PlayerPage);
            containerRegistry.RegisterForNavigation<LiveGamesPage, LiveGamesViewModel>(NavigationConstants.LiveGamesPage);
            containerRegistry.RegisterForNavigation<AboutPage, AboutViewModel>(NavigationConstants.AboutPage);

            containerRegistry.Register<IApiManager, ApiManager>();
            containerRegistry.RegisterInstance<IApiService<ILiveGamesFootballApi>>(new ApiService<ILiveGamesFootballApi>(Config.FootballApiUrl));
            containerRegistry.RegisterInstance<IApiService<IPlayersFootballApi>>(new ApiService<IPlayersFootballApi>(Config.FootballApiUrl));
            containerRegistry.RegisterInstance<IApiService<IStatisticsFootballApi>>(new ApiService<IStatisticsFootballApi>(Config.FootballApiUrl));
            containerRegistry.RegisterInstance<IApiService<ITeamsFootballApi>>(new ApiService<ITeamsFootballApi>(Config.FootballApiUrl));
            containerRegistry.RegisterInstance<IApiService<ITrophiesFootballApi>>(new ApiService<ITrophiesFootballApi>(Config.FootballApiUrl));
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync($"{NavigationConstants.NagivationPage}/{NavigationConstants.HomePage}");
        }
    }
}
