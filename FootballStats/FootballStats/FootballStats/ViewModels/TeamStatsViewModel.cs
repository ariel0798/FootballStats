using FootballStats.Services.Interfaces;

namespace FootballStats.ViewModels
{
    public class TeamStatsViewModel : BaseViewModel
    {
        public TeamStatsViewModel(IApiManager apiManager) : base(apiManager)
        {
        }
    }
}
