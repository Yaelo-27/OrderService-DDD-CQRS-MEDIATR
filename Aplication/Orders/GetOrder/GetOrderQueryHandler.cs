using Aplication.dto;
using Domain.Orders;
using MediatR;

namespace Aplication.Orders
{
    public sealed class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            OrderId orderId = new(request.OrderId);
            
            Order order = await _orderRepository.GetByIdAsync(orderId) ?? throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");
            // Map the Order domain entity to an OrderDto to return as the response. This involves extracting the relevant properties from the Order entity and its related entities (like ShippingContact and ShippingAddress) and populating the corresponding fields in the OrderDto, including transforming the list of OrderItems into a list of OrderItemDtos.
            //TODO: Consider using a mapping library like AutoMapper for more complex mappings, especially if the Order entity has many properties or if there are nested objects that also need to be mapped to DTOs. This can help reduce boilerplate code and improve maintainability.
            return new OrderDto
            {
                ShippingContactName = order.ShippingContact.Name,
                ShippingContactEmail = order.ShippingContact.Email,
                ShippingContactPhone = order.ShippingContact.PhoneNumber.Value,
                ShippingAddressStreet = order.ShippingAddress.Street,
                ShippingAddressCity = order.ShippingAddress.City,
                ShippingAddressState = order.ShippingAddress.State,
                ShippingAddressPostalCode = order.ShippingAddress.PostalCode,
                Status = order.Status.ToString(),
                Items = [.. order.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })]
            };
        }
    }
}