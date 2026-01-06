
using EshopModulith.Shared.Pagination;

namespace EshopModulith.Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

    public record GetProductsResult(PaginatedResult<ProductDto> Products);

    internal class GetProductsHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.pageIndex;
            var pageSize = query.PaginationRequest.pageSize;

            var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

            var products = await dbContext.Products.AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var productDtos = products.Adapt<List<ProductDto>>();// Mapster mapping in Compile time

            return new GetProductsResult(new PaginatedResult<ProductDto>(pageIndex,pageSize,totalCount,productDtos));
        }

        
    }
}
