using FootballStats.Models.Fixtures;
using FootballStats.Models.Players;
using FootballStats.Models.Teams;
using FootballStats.Models.Trophies;
using Refit;
using System.Threading.Tasks;
using FootballStats.Models.Leagues;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IRefitFootballApiService
    {
        [Get("/teams/team/{id}")]
        Task<Teams> GetTeamById(int id);

        [Get("/teams/league/{leagueId}")]
        Task<Teams> GetTeamByLeagueId(int leagueId);

        [Get("/players/player/{playerId}/2020")]
        Task<Players> GetPlayerStatsById(int playerId);

        [Get("/fixtures/live")]
        Task<Fixtures> GetFixturesLive();

        [Get("/trophies/player/{playerId}")]
        Task<Trophies> GetTrophiesById(int playerId);

        [Get("/leagues/team/{teamId}")]
        Task<Leagues> GetLeaguesById(int teamId);

    }
}
