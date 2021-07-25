using IISTestApplication.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IISTestApplication.Services
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken token = default)
        {
            var result = await _distributedCache.GetStringAsync(key, token);
            if (result is not null)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }

            return default(T);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken token = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), options, token);
        }
    }
}
