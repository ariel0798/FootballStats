using FootballStats.Services.Interfaces;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(IApiManager apiManager) : base(apiManager)
        {
        }
    }
}
