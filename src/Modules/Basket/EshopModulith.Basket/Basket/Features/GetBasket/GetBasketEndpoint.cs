
namespace EshopModulith.Basket.Basket.Features.GetBasket
{
    //public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCartDto ShoppingCart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var query = new GetBasketQuery(userName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetBasketResponse>();
                return Results.Ok(response);
            }).Produces<GetBasketResponse>() 
              .ProducesProblem(StatusCodes.Status404NotFound)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Get Basket")
              .WithDescription("Get Basket by UserName")
              .RequireAuthorization();
        }

    }
}
