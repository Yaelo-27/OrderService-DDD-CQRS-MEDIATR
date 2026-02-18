namespace Aplication.dto
{
    public record OrderDto
    {
        public string? ShippingContactName { get; init; }
        public string? ShippingContactEmail { get; init; }
        public string? ShippingContactPhone { get; init; }
        public string? ShippingAddressStreet { get; init; }
        public string? ShippingAddressCity { get; init; }
        public string? ShippingAddressState { get; init; }
        public string? ShippingAddressPostalCode { get; init; }
        public string? Status { get; init; }
        public IEnumerable<OrderItemDto> Items { get; init; } = [];
    }
}