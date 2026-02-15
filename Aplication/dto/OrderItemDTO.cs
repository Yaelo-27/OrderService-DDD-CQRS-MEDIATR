namespace Aplication.dto
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string? ProductName { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
    }
}