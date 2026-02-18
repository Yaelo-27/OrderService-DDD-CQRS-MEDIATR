namespace Domain.ValueObjects
{
    // This enum represents the different statuses that an order can have in the system.
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled   
    }
}