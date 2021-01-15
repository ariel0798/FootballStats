using Fusillade;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class TrophiesApiManager : BaseApiManager, ITrophiesApiManager
    {
        readonly IApiService<ITrophiesFootballApi> trophiesFootballApi;

        public TrophiesApiManager(IApiService<ITrophiesFootballApi> trophiesFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity)
            : base (userDialogs,connectivity)
        {
            this.trophiesFootballApi = trophiesFootballApi;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public async Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (trophiesFootballApi.GetApi(Priority.UserInitiated).GetTrophiesByPlayerId(playerId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
    }
}
