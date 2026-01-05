namespace EshopModulith.Shared.DDD
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {
    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; } //the list of the Domain Events of that aggergate
        IDomainEvent[] ClearDomainEvents(); //Clear and return all the domain events of that aggregate
    }
}
