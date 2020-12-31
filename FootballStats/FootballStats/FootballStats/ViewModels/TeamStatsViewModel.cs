using FootballStats.Services.Interfaces;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStats.ViewModels
{
    public class TeamStatsViewModel : BaseViewModel
    {
        public TeamStatsViewModel(IApiManager apiManager) : base(apiManager)
        {
        }
    }
}
