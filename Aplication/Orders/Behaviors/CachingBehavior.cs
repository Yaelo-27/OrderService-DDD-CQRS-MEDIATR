using Aplication.Interfaces;
using MediatR;

namespace Aplication.Orders.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
where TRequest : IRequest<TResponse>
{
    private readonly ICacheService _cache;
    public CachingBehavior(ICacheService cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       if(request is not ICacheableQuery cacheable)
        {
            return await next();
        }

        var cacheStored = await _cache.GetCacheAsync<TResponse>(cacheable.CacheKey);

        if(cacheStored is not null) return cacheStored;

        var response = await next();

        await _cache.SetCacheAsync<TResponse>(cacheable.CacheKey, response, cacheable.Expiration);

        return response;
    }
}