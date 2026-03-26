using EshopModulith.Basket.Data.Processors;
using EshopModulith.Shared.Data;
using EshopModulith.Shared.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EshopModulith.Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services related to the Basket module here

            //Application Use Case services 
            services.AddScoped<IBasketRepository, BasketRepository>();

            #region The Manual Way Of Register The Caching Repository 
            ////this will be ambiguous because we have two implementation for the same interface
            //services.AddScoped<IBasketRepository, CachedBasketRepository>();

            ////To resolve the ambiguity we will use factory method
            //services.AddScoped<IBasketRepository>(sp =>
            //{
            //    var repository = sp.GetRequiredService<BasketRepository>();
            //    var cache = sp.GetRequiredService<IDistributedCache>();
            //    return new CachedBasketRepository(repository, cache);
            //});
            #endregion

            //The Better Way Using Decorator Pattern Extension Method
            services.Decorate<IBasketRepository, CachedBasketRepository>();

            //Data-Infrastructure services 
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddHostedService<OutboxProcessor>();

            return services;
        }
        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {
            // Register services related to the Basket module here


            //Use Data - Infrastructure services
            app.UseMigration<BasketDbContext>();
            return app;
        }
    }
}
