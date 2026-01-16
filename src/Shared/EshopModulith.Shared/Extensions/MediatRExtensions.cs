using EshopModulith.Shared.Behavior;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EshopModulith.Shared.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });
            return services;
        }
    }
}
