
namespace EshopModulith.Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto Product);
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //Using FirstOrDefaultAsync instead of FindAsync to allow AsNoTracking
            var product = await dbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken);

            if (product == null)
                throw new Exception($"Product with ID {query.ProductId} not found.");

            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
