using Fusillade;

namespace FootballStats.Services.Interfaces
{
    public interface IApiService<T>
    {
        T GetApi(Priority priority);
    }
}
