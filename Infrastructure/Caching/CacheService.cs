using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Caching
{
    internal class CacheService(IMemoryCache memoryCache) : ICachedService
    {
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(10);

        public async Task<T> GetOrCreateAsync<T>(string key, 
            Func<CancellationToken, Task<T>> factory, 
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default)
        {
            T result = await memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
                    return factory(cancellationToken);
                }
            );

            return result;
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }

        public T Update<T>(string key, T value)
        {
            return memoryCache.Set(key, value);
        }

        public T Create<T>(string key, T value, TimeSpan? expiration)
        {
            return memoryCache.Set(key, value, expiration ?? DefaultExpiration);
        }
    }
}
