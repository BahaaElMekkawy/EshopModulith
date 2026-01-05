
namespace EshopModulith.Catalog.Products.EventsHandlers
{
    public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger) : INotificationHandler<ProductCreatedEvent>
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
