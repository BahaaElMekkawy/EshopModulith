

namespace EshopModulith.Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto product):ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(command.product.Id,cancellationToken);
            if (product is null)
            {
                throw new Exception($"Product with id {command.product.Id} not found.");
            }

            UpdateProductWithNewValues(product, command.product);

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
