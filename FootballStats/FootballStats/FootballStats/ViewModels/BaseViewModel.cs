using Acr.UserDialogs;
using FootballStats.Constants;
using FootballStats.Services.Interfaces;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public abstract class  BaseViewModel : INotifyPropertyChanged
    {
        public bool IsBusy { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected IUserDialogs pageDialogs;
        protected INavigationService navigationService;
        
        protected BaseViewModel(IUserDialogs userDialogs, INavigationService navigationService)
        {
            pageDialogs = userDialogs;
            this.navigationService = navigationService;
        }

        protected async Task RunSafe(Task task, bool ShowLoading = true, string loadingMessage = null)
        {
            try
            {
                if (IsBusy) 
                    return;

                IsBusy = true;

                if (ShowLoading)
                    pageDialogs.ShowLoading(loadingMessage ?? DialogResponsesConstants.Loading);

                await task;
            }
            catch (Exception e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                await pageDialogs.AlertAsync(DialogResponsesConstants.Error, DialogResponsesConstants.CheckInternetConnection , DialogResponsesConstants.Ok);
            }
            finally
            {
                IsBusy = false;
                if (ShowLoading)
                    pageDialogs.HideLoading();
            }
        }
    }
}
