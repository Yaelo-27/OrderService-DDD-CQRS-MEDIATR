namespace Aplication.Interfaces;
public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}