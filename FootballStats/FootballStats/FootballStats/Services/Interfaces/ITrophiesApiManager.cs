using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface ITrophiesApiManager
    {
        Task<HttpResponseMessage> GetTrophiesByPlayerId(int playerId);
    }
}
