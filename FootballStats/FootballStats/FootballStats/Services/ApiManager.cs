using FootballStats.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Acr.UserDialogs;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Fusillade;

namespace FootballStats.Services
{
    public class ApiManager : IApiManager
    {
        readonly IUserDialogs userDialogs = UserDialogs.Instance;
        readonly IConnectivity connectivity = CrossConnectivity.Current;
        readonly IApiService<IFootballApi> footballApi;
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        readonly Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();

        public ApiManager(IApiService<IFootballApi> footballApi)
        {
            this.footballApi = footballApi;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!e.IsConnected)
            {
                //Cancell All running task
                var items = runningTasks.ToList();
                foreach(var item in items)
                {
                    item.Value.Cancel();
                    runningTasks.Remove(item.Key);
                }
            }
        }
        protected async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
            where TData : HttpResponseMessage, 
            new()
        {
            var data = new TData();

            if (!IsConnected)
            {
                var strngResponse = "There's not a network connection";
                data.StatusCode = HttpStatusCode.BadRequest;
                data.Content = new StringContent(strngResponse);

                userDialogs.Toast(strngResponse, TimeSpan.FromSeconds(1));
                return data;
            }

            IsReachable = await connectivity.IsRemoteReachable(Config.FootballApiUrl);

            if (!IsReachable)
            {
                var strngResponse = "There's not an internet connection";
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

        public async Task<HttpResponseMessage> GetTeamByTeamId(int id)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetTeamByTeamId(id));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetTeamByLeagueId(leagueId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetPlayerStatsByPlayerId(int playerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetPlayerStatsByPlayerId(playerId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetFixturesLive()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetFixturesLive());

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetTrophiesByPlayerId(playerId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetLeaguesByTeamId(int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetLeaguesByTeamId(teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }

        public async Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetPlayersStatsByTeamId(teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (footballApi.GetApi(Priority.UserInitiated).GetTeamStatisticsByLeagueIdAndTeamId(leagueId, teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
    }
}
