using Aplication.Common.Constants;
using Aplication.dto;
using Aplication.Interfaces;

namespace Aplication.Orders
{
    public record GetOrderQuery(Guid OrderId) : IQuery<OrderDto>, ICacheableQuery
    {
        public string CacheKey => string.Format(AplicationConst.QueryOrderCachedKey, OrderId.ToString());
        public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
    }
}