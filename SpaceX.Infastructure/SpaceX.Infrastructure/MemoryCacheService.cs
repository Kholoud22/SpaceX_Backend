using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace SpaceX.Infrastructure
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public MemoryCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T> GetItemFromCache<T>(string itemKey)
        {
            var cachedItem = await _cache.GetStringAsync(itemKey);

            if (string.IsNullOrEmpty(cachedItem))
                return default(T)!;

            return JsonSerializer.Deserialize<T>(cachedItem)!;
        }

        public async Task<bool> SaveItemToCache<T>(string itemKey, T value)
        {
            var toBeCached = JsonSerializer.Serialize<T>(value);
            await _cache.SetStringAsync(itemKey, toBeCached, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
            return true;
        }
    }
}
