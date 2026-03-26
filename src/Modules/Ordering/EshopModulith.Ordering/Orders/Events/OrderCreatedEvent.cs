using EshopModulith.Ordering.Orders.Models;

namespace EshopModulith.Ordering.Orders.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvent
    {
    }
}
