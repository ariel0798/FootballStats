using Fusillade;
using System.Net.Http;
using Acr.UserDialogs;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class TeamsApiManager : BaseApiManager, ITeamsApiManager
    {
        readonly IApiService<ITeamsFootballApi> teamsFootballApi;

        public TeamsApiManager(IApiService<ITeamsFootballApi> teamsFootballApi,
            IUserDialogs userDialogs, IConnectivity connectivity) 
            : base(userDialogs, connectivity)
        {
            this.teamsFootballApi = teamsFootballApi;
            IsConnected = connectivity.IsConnected;
            connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public async Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId)
        {
            var cts = new CancellationTokenSource();
            var task = RemoteRequestAsync<HttpResponseMessage>
                (teamsFootballApi.GetApi(Priority.UserInitiated).GetTeamByLeagueId(leagueId));

            runningTasks.Add(task.Id, cts);

            return await task;
        }
    }
}
