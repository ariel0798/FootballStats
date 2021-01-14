using Polly;
using System;
using Fusillade;
using System.Net;
using System.Linq;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using FootballStats.Constants;
using System.Collections.Generic;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class ApiManager : IApiManager
    {
        public bool IsConnected { get; set; }
        public bool IsReachable { get; set; }
        readonly IUserDialogs userDialogs;
        readonly IConnectivity connectivity;
        readonly IApiService<ILiveGamesFootballApi> liveGamesFootballApi;
        readonly IApiService<IPlayersFootballApi> playersFootballApi;
        readonly IApiService<IStatisticsFootballApi> statisticsFootballApi;
        readonly IApiService<ITeamsFootballApi> teamsFootballApi;
        readonly IApiService<ITrophiesFootballApi> trophiesFootballApi;
        readonly Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();

        public ApiManager(IApiService<ILiveGamesFootballApi> liveGamesFootballApi,
            IApiService<IPlayersFootballApi> playersFootballApi,
            IApiService<IStatisticsFootballApi> statisticsFootballApi,
            IApiService<ITeamsFootballApi> teamsFootballApi,
            IApiService<ITrophiesFootballApi> trophiesFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity)
        {
            this.liveGamesFootballApi = liveGamesFootballApi;
            this.playersFootballApi = playersFootballApi;
            this.statisticsFootballApi = statisticsFootballApi;
            this.teamsFootballApi = teamsFootballApi;
            this.trophiesFootballApi = trophiesFootballApi;
            this.userDialogs = userDialogs;
            this.connectivity = connectivity;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public async Task<HttpResponseMessage> GetFixturesLive()
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (liveGamesFootballApi.GetApi(Priority.UserInitiated).GetFixturesLive());

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (playersFootballApi.GetApi(Priority.UserInitiated).GetPlayersStatsByTeamId(teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (statisticsFootballApi.GetApi(Priority.UserInitiated).GetTeamStatisticsByLeagueIdAndTeamId(leagueId, teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (teamsFootballApi.GetApi(Priority.UserInitiated).GetTeamByLeagueId(leagueId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
        public async Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (trophiesFootballApi.GetApi(Priority.UserInitiated).GetTrophiesByPlayerId(playerId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
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
        private async Task<TData> RemoteRequestAsync<TData>(Task<TData> task)
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
