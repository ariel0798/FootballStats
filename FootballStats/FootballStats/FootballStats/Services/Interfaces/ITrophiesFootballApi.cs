using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface ITrophiesFootballApi
    {
        [Get("/trophies/player/{playerId}")]
        Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId);
    }
}
