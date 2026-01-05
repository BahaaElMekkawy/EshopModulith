using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EshopModulith.Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Register ordering services, repositories, etc. here
            return services;
        }
        public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
        {
            // Register services related to the Basket module here
            return app;
        }
    }
}
