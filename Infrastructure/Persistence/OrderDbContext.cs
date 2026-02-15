using Aplication.data;
using Domain.Orders;
using Domain.Primitives.Domain;
using Domain.Primitives.Domain.Interaces;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class OrderDbContext : DbContext, IOrderDbContext, IUnitOfWork
    {
        private readonly IPublisher _mediator;
        public OrderDbContext(DbContextOptions<OrderDbContext> options, IPublisher mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)  
                .Where(e => e.DomainEvents.Any())
                .SelectMany(e => e.DomainEvents);
                
            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken); //publish the domain events after the changes have been saved to the database, ensuring that any side effects or additional processing triggered by the domain events occur only after the data has been successfully persisted, maintaining consistency and integrity of the application's state.
            }

            return result;
        }
    }
}