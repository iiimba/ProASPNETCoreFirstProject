using System.Threading;
using System.Threading.Tasks;

namespace IISTestApplication.Services.Interfaces
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key, CancellationToken token = default);

        Task SetAsync<T>(string key, T value, CancellationToken token = default);
    }
}
