
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EshopModulith.Basket.Data.Repository
{
    public class CachedBasketRepository(IBasketRepository repository , IDistributedCache cache) : IBasketRepository
    {
        //Use all these converts beacuse of the DDD rich models and private setters 
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonConverters.ShoppingCartConverter(),
                new JsonConverters.ShoppingCartItemConverter()
            }
        };

        public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            //if i need to get the for update or delete i will not use the cache
            if (!asNoTracking)
                return await repository.GetBasket(userName,false, cancellationToken);

            var chachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(chachedBasket))
                return  JsonSerializer.Deserialize<ShoppingCart>(chachedBasket,_options)!;

            var basket = await repository.GetBasket(userName,asNoTracking, cancellationToken);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket,_options), cancellationToken);

            return basket;
        }
        public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.CreateBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket,_options), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }

        public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
        {
            var result  = await repository.SaveChangesAsync(userName , cancellationToken);

            if (userName is not null)
                await cache.RemoveAsync(userName, cancellationToken);

            return result;
        }
    }
}
