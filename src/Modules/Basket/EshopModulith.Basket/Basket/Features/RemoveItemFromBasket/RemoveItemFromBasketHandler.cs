
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
    internal class RemoveItemFromBasketHandler(IBasketRepository repository) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.UserName, false, cancellationToken);

            basket.RemoveItem(command.ProductId);

            await repository.SaveChangesAsync(command.UserName , cancellationToken);

            return new RemoveItemFromBasketResult(basket.Id);
        }
    }

}
