using FootballStats.Models;
using Refit;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface IRefitFootballApiService
    {
        [Get("/v2/teams/team/{id}")]
        Task<Teams> GetTeamById(int id);
    }
}
