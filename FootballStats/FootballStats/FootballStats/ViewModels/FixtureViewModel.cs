﻿using FootballStats.Models.Fixtures;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class FixtureViewModel : BaseViewModel
    {
        public FixtureViewModel(IApiManager apiManager) : base(apiManager)
        {
            //Task.Run(async () => await RunSafe(GetData()));
            Title = "Live Games";
           GetMock();
        }
        public string Title { get; }

        public List<Fixture> Fixtures { get; set; }

        /*
        async Task GetData()
        {
            var footballResponse = await ApiManager.GetFixturesLive();

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var fixtures = await Task.Run(() => JsonConvert.DeserializeObject<Fixtures>(jsonResponse));
               
                Fixtures = fixtures.Api.Fixtures;
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }*/
       void GetMock()
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
