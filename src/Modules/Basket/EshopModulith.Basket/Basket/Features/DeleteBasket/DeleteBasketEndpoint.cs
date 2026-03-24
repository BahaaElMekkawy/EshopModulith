namespace EshopModulith.Basket.Basket.Features.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName); will Get it from URL parameters
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));

                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            }).Produces<DeleteBasketResponse>()
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Delete Basket")
              .WithDescription("Delete Basket")
              .RequireAuthorization();
        }
    }
}
