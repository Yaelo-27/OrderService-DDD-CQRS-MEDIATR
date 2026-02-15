using Aplication.dto;
using Aplication.Interfaces;

namespace Aplication.Orders
{
    public record GetOrderQuery(Guid OrderId) : IQuery<OrderDto>;
}