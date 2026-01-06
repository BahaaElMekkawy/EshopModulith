
namespace EshopModulith.Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Product.Name).NotEmpty().WithMessage("Name Is Required");
            RuleFor(p => p.Product.Id).NotEmpty().WithMessage("Id Is Required");
            RuleFor(p => p.Product.Price).GreaterThan(0).WithMessage("Price Must Be Greate Than 0");
        }
    }
    internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(command.Product.Id, cancellationToken);
           
            if (product is null)
                throw new ProductNotFoundException(command.Product.Id);

            UpdateProductWithNewValues(product, command.Product);

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDto productDto)
        {
            product.UpdateDetails(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price);
        }
    }
}
