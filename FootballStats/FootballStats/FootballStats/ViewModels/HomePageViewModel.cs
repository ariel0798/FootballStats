

using FootballStats.Services.Interfaces;

namespace FootballStats.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(IApiManager apiManager) : base(apiManager)
        {
        }
    }
}
