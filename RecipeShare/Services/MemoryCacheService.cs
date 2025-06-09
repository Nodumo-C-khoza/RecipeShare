using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RecipeShare.Interfaces;

namespace RecipeShare.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _defaultOptions;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
            _defaultOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
        }

        public async Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> factory,
            TimeSpan? expiration = null
        )
        {
            if (_cache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }

            var value = await factory();

            var options = expiration.HasValue
                ? new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiration.Value)
                : _defaultOptions;

            _cache.Set(key, value, options);
            return value;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            // Note: MemoryCache doesn't support prefix-based removal
            // In a distributed cache like Redis, this would be implemented
            return Task.CompletedTask;
        }
    }
}
