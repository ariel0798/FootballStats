using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IStatisticsFootballApi
    {
        [Get("/statistics/{leagueId}/{teamId}")]
        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);
    }
}
