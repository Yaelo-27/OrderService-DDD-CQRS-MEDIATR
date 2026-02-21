using System.Text.Json;
using Aplication.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Persistence.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T>? GetCacheAsync<T>(string key)
        {
           var cachedData = await _cache.GetStringAsync(key);
           
           if(cachedData is null) return default;

           return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
           var serializedValue = JsonSerializer.Serialize(value);
           await _cache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
           {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(7)
           }); 
        }

    }
}