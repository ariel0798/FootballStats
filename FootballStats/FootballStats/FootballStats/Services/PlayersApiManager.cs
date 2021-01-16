using Fusillade;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class PlayersApiManager : BaseApiManager, IPlayersApiManager
    {
        readonly IApiService<IPlayersFootballApi> playersFootballApi;

        public PlayersApiManager(IApiService<IPlayersFootballApi> playersFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity)
            : base(userDialogs, connectivity)
        {
            this.playersFootballApi = playersFootballApi;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public async Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (playersFootballApi.GetApi(Priority.UserInitiated).GetPlayersStatsByTeamId(teamId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
    }
}
