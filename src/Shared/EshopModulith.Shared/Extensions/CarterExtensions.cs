using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EshopModulith.Shared.Extensions
{
    public static class CarterExtensions
    {
        // Extension method to add Carter modules from a specified assembly
        public static IServiceCollection AddCarterModulesFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCarter(configurator: config => 
            {
                foreach (var assembly in assemblies)
                {
                    var modules = assembly.GetTypes()
                        .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

                    config.WithModules(modules);
                }
            });
            return services;
        }
    }
}
