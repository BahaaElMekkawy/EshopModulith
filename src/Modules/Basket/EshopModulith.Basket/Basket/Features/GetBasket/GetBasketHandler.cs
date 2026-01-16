
namespace EshopModulith.Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);
    internal class GetBasketHandler(BasketDbContext dbContext) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await dbContext.ShoppingCarts.AsNoTrackingWithIdentityResolution().Include(c => c.Items)
                .SingleOrDefaultAsync(b => b.UserName == query.UserName, cancellationToken);

            if (basket == null)
                throw new BasketNotFoundException($"Basket for user '{query.UserName}' not found.");

            var basketDto = basket.Adapt<ShoppingCartDto>();
            return new GetBasketResult(basketDto);
        }
    }
}
