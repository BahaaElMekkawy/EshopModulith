namespace EshopModulith.Catalog.Products.EventsHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
    {
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            //publish to productPriceChanged integration event for other modules to consume
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }

    }
}
