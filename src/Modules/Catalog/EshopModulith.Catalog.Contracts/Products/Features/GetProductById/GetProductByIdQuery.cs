using EshopModulith.Shared.Contracts.CQRS;

namespace EshopModulith.Catalog.Contracts.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto Product);

}
