using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IApiManager
    {
        Task<HttpResponseMessage> GetTeamById(int id);

        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);

        Task<HttpResponseMessage> GetPlayerStatsById(int playerId);

        Task<HttpResponseMessage> GetFixturesLive();

        Task<HttpResponseMessage> GetTrophiesById(int playerId);

        Task<HttpResponseMessage> GetLeaguesById(int teamId);
    }
}
