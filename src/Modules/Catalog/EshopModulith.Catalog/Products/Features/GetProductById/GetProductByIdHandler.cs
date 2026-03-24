
using EshopModulith.Catalog.Contracts.Products.Features.GetProductById;

namespace EshopModulith.Catalog.Products.Features.GetProductById
{
    internal class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //Using FirstOrDefaultAsync instead of FindAsync to allow AsNoTracking
            var product = await dbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken);

            if (product == null)
                throw new ProductNotFoundException(query.ProductId);

            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
