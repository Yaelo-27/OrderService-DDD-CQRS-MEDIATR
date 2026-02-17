using Domain.Orders;
using Domain.Product;

namespace Domain.ValueObjects;
public partial record OrderItem
{
    private OrderItem() { }
    private OrderItem(ProductId productId, string? productName, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public static OrderItem? Create(ProductId productId, string? productName, int quantity, decimal unitPrice)
    {
        if (productId == null || productName  == null || quantity <= 0 || unitPrice < 0)
        {
            return null; // Return null if any of the required fields are invalid, ensuring that only valid order items are created.
        }

        return new OrderItem(productId, productName, quantity, unitPrice);
    }
    public ProductId ProductId { get; init; }
    public string? ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}