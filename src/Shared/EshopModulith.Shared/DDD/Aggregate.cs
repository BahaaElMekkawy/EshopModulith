namespace EshopModulith.Shared.DDD
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new(); //private to prevent outside modification
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly(); //expose them as readonly 
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvent[] ClearDomainEvents() //returns the cleared events for further processing if needed and also prevent duplicate publisihing
        {
            IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return dequeuedEvents;
        }
    }
}
