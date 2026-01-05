namespace EshopModulith.Catalog.Products.Features.GetProductById
{
    //public record GetProductByIdRequest(Guid Id); No request object needed as the id is passed as a route parameter
    public record GetProductByIdResponse(ProductDto Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);
                var product = await sender.Send(query);
                var response = product.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            }).WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Gets a product by its unique identifier.");
        }
    }
}
