using EshopModulith.Shared.Messaging.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EshopModulith.Basket.Basket.Features.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout)  : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckout).NotNull().WithMessage("BasketCheckoutDto Can't Be null");
            RuleFor(x => x.BasketCheckout.UserName).NotEmpty().WithMessage("Username is required");
        }
    }
    internal class CheckoutBasketHandler (BasketDbContext dbContext)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            await using var transaction =
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                // Get existing basket with total price
                var basket = await dbContext.ShoppingCarts
                    .Include(x => x.Items)
                    .SingleOrDefaultAsync(x => x.UserName == command.BasketCheckout.UserName, cancellationToken);

                if (basket == null)
                    throw new BasketNotFoundException(command.BasketCheckout.UserName);

                // Set total price on basket checkout event message
                var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();
                eventMessage.TotalPrice = basket.TotalPrice;

                // Write a message to the outbox
                var outboxMessage = new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = typeof(BasketCheckoutIntegrationEvent).AssemblyQualifiedName!,
                    Content = JsonSerializer.Serialize(eventMessage),
                    OccuredOn = DateTime.UtcNow
                };

                dbContext.OutboxMessages.Add(outboxMessage);

                // Delete the basket
                dbContext.ShoppingCarts.Remove(basket);

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new CheckoutBasketResult(true);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                return new CheckoutBasketResult(false);
            }


            //Checkout without the outbox pattern 
            ////get existing basket with total price
            //var basket = await basketRepository.GetBasket(command.BasketCheckout.UserName, true , cancellationToken);

            ////set total price on BasketCheckoutDto with event message
            //var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();

            //eventMessage.TotalPrice = basket.TotalPrice;

            ////send basket checkout event to rabbitmq
            //await bus.Publish(eventMessage, cancellationToken);

            ////delete basket
            //await basketRepository.DeleteBasket(command.BasketCheckout.UserName, cancellationToken);

            //return new CheckoutBasketResult(true);
        }
    }
}
