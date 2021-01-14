using Acr.UserDialogs;
using FootballStats.Constants;
using FootballStats.Services.Interfaces;
using Prism.Navigation;

namespace FootballStats.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Title { get; }

        public AboutViewModel(IApiManager apiManager,
            IUserDialogs userDialogs, INavigationService navigationService)
            : base(apiManager, userDialogs, navigationService)
        {
            Title = PageTitlesConstants.AboutUs;
        }        
    }
}
