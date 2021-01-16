using Fusillade;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class StatisticsApiManager : BaseApiManager, IStatisticsApiManager
    {
        readonly IApiService<IStatisticsFootballApi> statisticsFootballApi;

        public StatisticsApiManager(IApiService<IStatisticsFootballApi> statisticsFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity)
            : base(userDialogs, connectivity)
        {
            this.statisticsFootballApi = statisticsFootballApi;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public async Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (statisticsFootballApi.GetApi(Priority.UserInitiated).GetTeamStatisticsByLeagueIdAndTeamId(leagueId, teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
    }
}
