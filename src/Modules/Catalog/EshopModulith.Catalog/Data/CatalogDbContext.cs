namespace EshopModulith.Catalog.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) :
            base(options) { }

        // Exposes the Product DbSet via Set<Product>().
        // Allows Add/Update/Remove/Query, but prevents reassignment of the DbSet reference.
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("catalog");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
