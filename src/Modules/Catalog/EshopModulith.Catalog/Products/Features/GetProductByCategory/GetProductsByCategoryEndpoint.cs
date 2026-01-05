
namespace EshopModulith.Catalog.Products.Features.GetProductByCategory
{
    //public record GetProductByCategoryRequest(string Category); will get the category as a parameter

    public record GetProductsByCategoryResponse(IEnumerable<ProductDto> Products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var products = await sender.Send(query);
                var response = products.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            }).WithName("GetProductsByCategory")
              .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .WithSummary("Get Products By Category")
              .WithDescription("Get All Products With The Same Category");
        }
    }
}
