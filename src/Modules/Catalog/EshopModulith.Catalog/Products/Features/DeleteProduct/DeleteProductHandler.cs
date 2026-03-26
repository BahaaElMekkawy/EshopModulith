
namespace EshopModulith.Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product Id Is Required");
        }
    }

    internal class DeleteProductHandler(CatalogDbContext dbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(command.ProductId, cancellationToken);

            if (product is null)
                throw new ProductNotFoundException(command.ProductId);

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
