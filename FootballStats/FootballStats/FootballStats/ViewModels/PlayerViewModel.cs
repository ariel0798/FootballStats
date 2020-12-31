using FootballStats.Services.Interfaces;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStats.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(IApiManager apiManager) : base(apiManager)
        {
        }
    }
}
