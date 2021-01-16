using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface ITeamsApiManager
    {
        Task<HttpResponseMessage> GetTeamByLeagueId(int leagueId);
    }
}
