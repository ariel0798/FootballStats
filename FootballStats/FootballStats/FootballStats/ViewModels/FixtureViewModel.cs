﻿using FootballStats.Models.Fixtures;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class FixtureViewModel : BaseViewModel
    {
        public FixtureViewModel(IApiManager apiManager) : base(apiManager)
        {
            //Task.Run(async () => await RunSafe(GetMock()));
            GetMock();
        }

        public Fixture Fixture { get; set; }

        public List<Fixture> Fixtures { get; set; }

       


       /* async Task GetData()
        {
            var footballResponse = await ApiManager.GetFixturesLive();

            if (footballResponse.IsSuccessStatusCode)
            {
                var jsonResponse = await footballResponse.Content.ReadAsStringAsync();
                var fixtures = await Task.Run(() => JsonConvert.DeserializeObject<Fixtures>(jsonResponse));
               
                Fixture = fixtures.Api.Fixtures[0];
            }
            else
            {
                await PageDialogs.AlertAsync("Unable to get data", "Error", "Ok");
            }
        }*/
       void GetMock()
        {
            Fixture.AwayTeam.TeamName = "Leicester";
            Fixture.AwayTeam.Logo = "https://media.api-sports.io/football/teams/46.png";
            Fixture.HomeTeam.TeamName = "Manchester United";
            Fixture.HomeTeam.Logo = "https://media.api-sports.io/football/teams/46.png";
            Fixture.Status = "FT";
            Fixture.GoalsAwayTeam = 2;
            Fixture.GoalsHomeTeam = 1;
        }

    }
}
