using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetTeamByTeamId(int id);

        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);

        Task<HttpResponseMessage> GetPlayerStatsByPlayerId(int playerId);

        Task<HttpResponseMessage> GetFixturesLive();

        Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId);

        Task<HttpResponseMessage> GetLeaguesByTeamId(int teamId);

        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);
        Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId);
        Task<HttpResponseMessage> GetLeagues();
    }
}
