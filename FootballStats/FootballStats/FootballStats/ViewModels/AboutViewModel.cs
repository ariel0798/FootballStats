using Acr.UserDialogs;
using FootballStats.Constants;
using Prism.Navigation;

namespace FootballStats.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Title { get; }

        public AboutViewModel(IUserDialogs userDialogs, INavigationService navigationService)
            : base(userDialogs, navigationService)
        {
            Title = PageTitlesConstants.AboutUs;
        }        
    }
}
