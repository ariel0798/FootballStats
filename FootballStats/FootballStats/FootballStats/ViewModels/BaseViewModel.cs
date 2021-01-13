using Acr.UserDialogs;
using FootballStats.Services.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FootballStats.ViewModels
{
    public abstract class  BaseViewModel : INotifyPropertyChanged
    {
        protected IUserDialogs PageDialogs = UserDialogs.Instance;
        protected IApiManager ApiManager;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public bool IsBusy { get; set; }
        public  BaseViewModel(IApiManager apiManager)
        {
            ApiManager = apiManager;
        }

        public async Task RunSafe(Task task, bool ShowLoading = true, string loadingMessage = null)
        {
            try
            {
                if (IsBusy) 
                    return;

                IsBusy = true;

                if (ShowLoading)
                    UserDialogs.Instance.ShowLoading(loadingMessage ?? "Loading");

                await task;
            }
            catch (Exception e)
            {
                IsBusy = false;
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(e.ToString());
                await App.Current.MainPage.DisplayAlert("Error", "Check your internet connection", "Ok");
            }
            finally
            {
                IsBusy = false;
                if (ShowLoading)
                    UserDialogs.Instance.HideLoading();
            }
        }
    }
}
