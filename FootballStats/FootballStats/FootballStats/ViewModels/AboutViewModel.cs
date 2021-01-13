using FootballStats.Services.Interfaces;

namespace FootballStats.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(IApiManager apiManager) : base(apiManager)
        {
            Title = "About Us";
        }

        public string Title { get; }
    }
}
