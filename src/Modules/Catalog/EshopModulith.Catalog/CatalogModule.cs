using EshopModulith.Shared.Behavior;
using EshopModulith.Shared.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EshopModulith.Catalog
{
    public static class CatalogModule
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services related to the Catalog module here
            //Add services to the container 

            //Add Api Endpint services

            //Add Application Use Case services
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Data-Infrastructure services 
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptors>();

            services.AddDbContext<CatalogDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });


            services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }
        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            //Register middleware related to the Catalog module here

            //Use Api Endpint services 

            //Use Application Use Case services

            //Use Data-Infrastructure services
            app.UseMigration<CatalogDbContext>();
            return app;
        }
    }
}
