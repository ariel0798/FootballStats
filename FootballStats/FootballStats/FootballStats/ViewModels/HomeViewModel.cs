using Acr.UserDialogs;
using Prism.Navigation;

namespace FootballStats.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IUserDialogs userDialogs, INavigationService navigationService)
            : base(userDialogs, navigationService)
        {
        }
    }
}
