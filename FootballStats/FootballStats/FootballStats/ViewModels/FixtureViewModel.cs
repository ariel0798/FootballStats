using FootballStats.Models.Fixtures;
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
        }

        public Fixture Fixture { get; set; }

        public List<Fixture> Fixtures { get; set; }

        public DelegateCommand GetFixtureDataCommand => new DelegateCommand(async () => await RunSafe(GetData()));


        async Task GetData()
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
        }

    }
}
