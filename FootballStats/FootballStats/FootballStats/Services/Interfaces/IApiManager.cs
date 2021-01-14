using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetFixturesLive();
        Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId);
        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);
        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);
        Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId);
    }
}
