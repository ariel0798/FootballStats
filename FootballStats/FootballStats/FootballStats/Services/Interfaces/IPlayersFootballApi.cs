using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IPlayersFootballApi
    {
        [Get("/players/team/{teamId}/2020")]
        Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId);
    }
}
