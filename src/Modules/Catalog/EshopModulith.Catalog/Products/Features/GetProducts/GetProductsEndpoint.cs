
using EshopModulith.Shared.Pagination;

namespace EshopModulith.Catalog.Products.Features.GetProducts
{
    public record GetProductsRequest(PaginationRequest PaginationRequest);

    public record GetProductsResponse(PaginatedResult<ProductDto> Products);
    public class GetProductsEndpoint() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetProductsQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            }).WithName("GetProducts")
              .Produces<GetProductsResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Products")
              .WithDescription("Get All Products");
        }
    }
}
