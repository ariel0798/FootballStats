using Acr.UserDialogs;
using FootballStats.Constants;
using FootballStats.Models.Fixtures;
using Prism.Navigation;
using System.Collections.Generic;

namespace FootballStats.ViewModels
{
    public class LiveGamesViewModel : BaseViewModel
    {
        public string Title { get; }
        public List<Fixture> Fixtures { get; set; }

        public LiveGamesViewModel(IUserDialogs userDialogs, INavigationService navigationService)
            : base( userDialogs, navigationService)
        {
            Title = PageTitlesConstants.LiveGames;
           GetMock();
        }
        
       private void GetMock()
       {
            Fixtures = new List<Fixture>();
            var fixture = new Fixture
            {
                AwayTeam = new AwayTeam(),
                HomeTeam = new HomeTeam(),
                Score = new Score()
            };

            fixture.AwayTeam.Logo = "https://media.api-sports.io/football/teams/46.png";
            fixture.HomeTeam.Logo = "https://media.api-sports.io/football/teams/33.png";
            fixture.GoalsAwayTeam = 2;
            fixture.GoalsHomeTeam = 1;
            fixture.Score.Fulltime = "2-1";

            Fixtures.Add(fixture);

            var fixture2 = new Fixture
            {
                AwayTeam = new AwayTeam(),
                HomeTeam = new HomeTeam(),
                Score = new Score()
            };

            fixture2.AwayTeam.Logo = "https://media.api-sports.io/football/teams/45.png";
            fixture2.HomeTeam.Logo = "https://media.api-sports.io/football/teams/34.png";
            fixture2.GoalsAwayTeam = 3;
            fixture2.GoalsHomeTeam = 0;
            fixture2.Score.Fulltime = "3-0";
            Fixtures.Add(fixture2);
        }
    }
}
