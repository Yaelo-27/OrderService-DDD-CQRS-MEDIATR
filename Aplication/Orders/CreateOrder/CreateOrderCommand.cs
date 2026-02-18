using Aplication.dto;
using Aplication.Interfaces;
using Domain.Orders;

namespace Aplication.Orders
{
    public record CreateOrderCommand(
        string ShippingContactName,
        string ShippingContactEmail,
        string ShippingContactPhoneNumber,
        string ShippingAddressStreet,
        string ShippingAddressCity,
        string ShippingAddressState,
        string ShippingAddressPostalCode,
        string ShippingAddressCountry,
        IEnumerable<OrderItemDto> Items
    ) : ICommand<OrderId>;
}