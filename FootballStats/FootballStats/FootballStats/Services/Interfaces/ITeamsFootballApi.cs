using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface ITeamsFootballApi
    {
        [Get("/teams/league/{leagueId}")]
        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);
    }
}
