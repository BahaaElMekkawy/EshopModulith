using EshopModulith.Shared.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EshopModulith.Shared.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();
            SeedDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();

            return app;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            await dbContext.Database.MigrateAsync();
        }

        private static async Task SeedDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();//Get Services to support multiple seeders
            foreach (var seeder in seeders)
            {
                await seeder.SeedAllAsync();
            }
        }
    }
}
