using Refit;
using System.Threading.Tasks;
using System.Net.Http;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IFootballApi
    {
        [Get("/teams/team/{id}")]
        Task<HttpResponseMessage> GetTeamById(int id);

        [Get("/teams/league/{leagueId}")]
        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);

        [Get("/players/player/{playerId}/2020")]
        Task<HttpResponseMessage> GetPlayerStatsById(int playerId);

        [Get("/fixtures/live")]
        Task<HttpResponseMessage> GetFixturesLive();

        [Get("/trophies/player/{playerId}")]
        Task<HttpResponseMessage> GetTrophiesById(int playerId);

        [Get("/leagues/team/{teamId}")]
        Task<HttpResponseMessage> GetLeaguesById(int teamId);

        [Get("/statistics/{leagueId}/{teamId}")]
        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);
    }
}
