using Refit;
using System.Threading.Tasks;
using System.Net.Http;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IFootballApi
    {
        [Get("/teams/team/{id}")]
        Task<HttpResponseMessage> GetTeamByTeamId(int id);

        [Get("/teams/league/{leagueId}")]
        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);

        [Get("/players/player/{playerId}/2020")]
        Task<HttpResponseMessage> GetPlayerStatsByPlayerId(int playerId);

        [Get("/fixtures/live")]
        Task<HttpResponseMessage> GetFixturesLive();

        [Get("/trophies/player/{playerId}")]
        Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId);

        [Get("/leagues/team/{teamId}")]
        Task<HttpResponseMessage> GetLeaguesByTeamId(int teamId);

        [Get("/leagues/season/2020")]
        Task<HttpResponseMessage> GetLeagues();

        [Get("/statistics/{leagueId}/{teamId}")]
        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);

        [Get("/players/team/{teamId}/2020")]
        Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId);
    }
}
