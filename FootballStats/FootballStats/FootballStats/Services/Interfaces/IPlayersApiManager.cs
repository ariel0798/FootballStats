using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IPlayersApiManager
    {
        Task<HttpResponseMessage> GetPlayersStatsByTeamId(int teamId);
    }
}
