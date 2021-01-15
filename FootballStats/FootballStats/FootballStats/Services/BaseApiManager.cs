using Polly;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using FootballStats.Constants;
using System.Collections.Generic;
using Plugin.Connectivity.Abstractions;

namespace FootballStats.Services
{
    public abstract class BaseApiManager
    {
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        protected internal IUserDialogs userDialogs;
        protected internal IConnectivity connectivity;
        protected internal Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();

        protected BaseApiManager(IUserDialogs userDialogs, IConnectivity connectivity)
        {
            this.userDialogs = userDialogs;
            this.connectivity = connectivity;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        protected internal void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!e.IsConnected)
            {
                //Cancell All running task
                var items = runningTasks.ToList();
                foreach (var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }

        protected internal async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage,
            new()
        {
            var data = new TData();

            if (!IsConnected)
            {
                var strngResponse = DialogResponsesConstants.NoNetworkConnection;
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            IsReachable = await connectivity.IsRemoteReachable(Config.FootballApiUrl);

            if (!IsReachable)
            {
                var strngResponse = DialogResponsesConstants.NoInternetConnection;
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            data = await Policy
                .Handle<WebException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync
                (
                    retryCount: 1,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .ExecuteAsync(async () =>
                {
                    var result = await task;

                    return result;
                });
            return data;
        }
    }
}
