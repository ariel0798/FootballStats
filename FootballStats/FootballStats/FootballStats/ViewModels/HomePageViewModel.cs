using Acr.UserDialogs;
using FootballStats.Models.Fixtures;
using FootballStats.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(IApiManager apiManager) : base(apiManager)
        {
        }

      

    }
}
