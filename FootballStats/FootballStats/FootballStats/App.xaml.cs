using FootballStats.Services;
using FootballStats.Services.Interfaces;
using FootballStats.ViewModels;
using FootballStats.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

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
            containerRegistry.RegisterForNavigation<HomePage,HomePageViewModel>(NavigationConstants.HomePage);
            containerRegistry.RegisterForNavigation<TeamStatsPage,TeamStatsViewModel>(NavigationConstants.TeamStatsPage);
            containerRegistry.RegisterForNavigation<PlayerPage,PlayerViewModel>(NavigationConstants.PlayerPage);
            containerRegistry.RegisterForNavigation<FixturePage, FixtureViewModel>(NavigationConstants.FixturePage);


            containerRegistry.RegisterInstance<IApiManager>(new ApiManager(new ApiService<IFootballApi>(Config.FootballApiUrl)));
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync($"{NavigationConstants.NagivationPage}/{NavigationConstants.HomePage}");

        }
    }
}
