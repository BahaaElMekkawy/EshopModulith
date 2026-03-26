using EshopModulith.Ordering.Orders.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EshopModulith.Ordering.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Domain Event Handled : {notification.GetType}");

            return Task.CompletedTask;
        }
    }
}
