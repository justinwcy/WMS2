namespace Infrastructure.Caching
{
    public interface ICachedService
    {
        public Task<T> GetOrCreateAsync<T>(string key, 
            Func<CancellationToken, Task<T>> factory, 
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default);

        public void Remove(string key);

        public T Update<T>(string key, T value);

        public T Create<T>(string key, T value, TimeSpan? expiration);
    }
}
