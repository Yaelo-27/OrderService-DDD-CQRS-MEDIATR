using Domain.Primitives.interfaces;

namespace Domain.Primitives.Domain
{
    // This class represents the base class for all aggregate roots in the domain model.
    public abstract class AggregateRoot
    {
        // A list to hold the domain events that have occurred within the aggregate root.
        private readonly List<IDomainEvent> _domainEvents = new();

        // A public read-only collection to expose the domain events to other parts of the application.
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        
        // A protected method to add a domain event to the list of domain events.
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}