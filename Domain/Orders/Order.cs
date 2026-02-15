using System.Diagnostics.Contracts;
using Domain.Primitives.Domain;
using Domain.ValueObjects;

namespace Domain.Orders
{
    public sealed class Order : AggregateRoot
    {
       private readonly List<OrderItem> _items = new();
       public Order(OrderId id, Contact shippingContact, Address shippingAddress, IEnumerable<OrderItem> items)
       {
           Id = id;
           ShippingContact = shippingContact;
           ShippingAddress = shippingAddress;
           _items = items?.ToList() ?? []; // Ensure that the items list is initialized to an empty list if null is passed, preventing potential null reference exceptions when accessing the order items.
           Status = OrderStatus.Pending; // Set the initial status of the order to Pending when it is created, indicating that the order is awaiting processing.
       }
       private Order() {} //Ef Core requires a parameterless constructor for materialization, but we make it private to prevent direct instantiation without using the factory method, ensuring that all Order instances are created through the Create method which can enforce validation rules.
       public OrderId Id { get; private set; }
       public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly(); // Expose items as a read-only collection to prevent external modification of the order items, ensuring that all changes to the order items go through controlled methods that can enforce business rules and maintain the integrity of the order.
       public OrderStatus Status { get; private set; }
       public Contact ShippingContact { get; private set; }
       public Address ShippingAddress { get; private set; }
       public decimal TotalAmount => _items.Sum(i => i.Quantity * i.UnitPrice);
    }
}