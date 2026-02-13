namespace Domain.Orders
{
    // This interface defines the contract for a repository that manages orders in the system.
    // It provides methods for adding, retrieving, updating, and deleting orders, as well as retrieving orders by their unique identifier.
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(OrderId orderId);
    }
}