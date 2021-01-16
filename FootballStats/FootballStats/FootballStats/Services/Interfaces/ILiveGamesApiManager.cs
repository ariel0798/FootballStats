using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface ILiveGamesApiManager
    {
        Task<HttpResponseMessage> GetFixturesLive();
    }
}
