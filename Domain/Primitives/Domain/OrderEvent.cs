using Domain.Orders;
using Domain.Primitives.interfaces;
using MediatR;

namespace Domain.Primitives.Domain
{
    public record OrderEvent : INotification, IDomainEvent
    {
        public DateTime OcurredOn { get; } = DateTime.UtcNow;
        public OrderId OrderId { get; }
        public string EventType { get; }

        public OrderEvent(OrderId orderId, string eventType)
        {
            OrderId = orderId;
            EventType = eventType;
        }
    }
}