using Fusillade;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class LiveGamesApiManager : BaseApiManager, ILiveGamesApiManager
    {
        readonly IApiService<ILiveGamesFootballApi> liveGamesFootballApi;

        public LiveGamesApiManager(IApiService<ILiveGamesFootballApi> liveGamesFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity)
            : base(userDialogs, connectivity)
        {
            this.liveGamesFootballApi = liveGamesFootballApi;
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
    }
}
