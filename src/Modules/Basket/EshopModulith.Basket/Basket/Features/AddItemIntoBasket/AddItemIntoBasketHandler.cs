
namespace EshopModulith.Basket.Basket.Features.AddItemIntoBasket
{

    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto Item) : ICommand<AddItemIntoBasketResult>;
    public record AddItemIntoBasketResult(Guid Id);
    public class AddItemIntoBasketCommandValidator : AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemIntoBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName Is Required");
            RuleFor(x => x.Item.ProductId).NotEmpty().WithMessage("ProductId Is Required");
            RuleFor(x => x.Item.Quantity).GreaterThan(0).WithMessage("Quantity Must Be Greater Than 0");
        }
    }
    internal class AddItemIntoBasketHandler(BasketDbContext dbContext) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.UserName == command.UserName, cancellationToken);

            if (basket == null)
                throw new BasketNotFoundException(command.UserName);
            basket.AddItem(command.Item.ProductId, command.Item.Quantity, command.Item.Color, command.Item.Price, command.Item.ProductName);

            await dbContext.SaveChangesAsync(cancellationToken);
            return new AddItemIntoBasketResult(basket.Id);  
        }
    }
}
