namespace EshopModulith.Catalog.Products.Features.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            }).WithName("Delete Product")
              .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Delete Product")
              .WithDescription("Deletes a product from the catalog.");
        }
    }
}
