using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    public interface IApiCallerService
    {
        Task RunSafe(Task task, bool ShowLoading = true, string loadingMessage = null);
    }
}
