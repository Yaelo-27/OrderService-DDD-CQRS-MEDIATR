using Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace Aplication.data
{
    public interface IOrderDbContext
    {
        DbSet<Order> Order { get; set;}
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}