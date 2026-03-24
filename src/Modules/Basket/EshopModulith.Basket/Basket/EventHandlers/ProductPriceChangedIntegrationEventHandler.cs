using EshopModulith.Basket.Basket.Features.UpdateItemPriceInBasket;
using EshopModulith.Shared.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EshopModulith.Basket.Basket.EventHandlers
{
    public class ProductPriceChangedIntegrationEventHandler(
        ILogger<ProductPriceChangedIntegrationEventHandler> logger, ISender sender)
        : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            logger.LogInformation($"Integration Event Handled {context.Message.GetType().Name}");

            //MediatR could be used here to send a command to update the product prices in the basket
            var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);

            var result = await sender.Send(command);

            if(!result.IsSuccess)
                logger.LogError($"Failed to update item price in basket for product {context.Message.ProductId}");
            
            logger.LogInformation($"Price for productId updated in bakset{context.Message.ProductId}");
        }
    }
}
