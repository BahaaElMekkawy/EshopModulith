
using EshopModulith.Catalog.Contracts.Products.Features.GetProductById;
using EshopModulith.Shared.Contracts.CQRS;

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
    internal class AddItemIntoBasketHandler(IBasketRepository repository,ISender sender) : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.UserName, false, cancellationToken);

            var productDto = await sender.Send(new GetProductByIdQuery(command.Item.ProductId));

            basket.AddItem(
                command.Item.ProductId, 
                command.Item.Quantity,
                command.Item.Color,
                productDto.Product.Price,
                productDto.Product.Name);

            await repository.SaveChangesAsync(command.UserName,cancellationToken);

            return new AddItemIntoBasketResult(basket.Id);
        }
    }
}
