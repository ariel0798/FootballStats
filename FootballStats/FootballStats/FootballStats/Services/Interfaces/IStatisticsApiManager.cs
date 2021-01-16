using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IStatisticsApiManager
    {
        Task<HttpResponseMessage> GetTeamStatisticsByLeagueIdAndTeamId(int leagueId, int teamId);
    }
}
