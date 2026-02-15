using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
   public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Order order) => await _context.Order.AddAsync(order);

        public async Task<Order?> GetByIdAsync(OrderId orderId)  => await _context.Order.SingleOrDefaultAsync(o => o.Id == orderId);
       
    }  
}