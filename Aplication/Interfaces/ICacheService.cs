namespace Aplication.Interfaces
{
   public interface ICacheService
    {
        Task<T>? GetCacheAsync<T>(string key);
        Task SetCacheAsync<T>(string key, T value, TimeSpan? expiration = null);
        Task RemoveCacheAsync(string key);
    } 
}
