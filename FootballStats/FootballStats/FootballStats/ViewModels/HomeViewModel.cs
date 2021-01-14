using Acr.UserDialogs;
using FootballStats.Services.Interfaces;
using Prism.Navigation;

namespace FootballStats.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IApiManager apiManager,
            IUserDialogs userDialogs, INavigationService navigationService)
            : base(apiManager, userDialogs, navigationService)
        {
        }
    }
}
