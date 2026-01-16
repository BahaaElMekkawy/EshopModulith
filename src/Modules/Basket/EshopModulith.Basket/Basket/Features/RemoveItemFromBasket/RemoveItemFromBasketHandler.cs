
namespace EshopModulith.Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid Id);
    public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
    {
        public RemoveItemFromBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName Is Required");
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId Is Required");
        }
    }
    internal class RemoveItemFromBasketHandler(BasketDbContext dbContext) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts
                .Include(b => b.Items)
                .SingleOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

            if (basket is null)
                throw new BasketNotFoundException(command.UserName);

            basket.RemoveItem(command.ProductId);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new RemoveItemFromBasketResult(basket.Id);
        }
    }

}
