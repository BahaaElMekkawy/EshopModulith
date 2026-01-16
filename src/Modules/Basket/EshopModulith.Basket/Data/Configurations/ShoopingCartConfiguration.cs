using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EshopModulith.Basket.Data.Configurations
{
    public class ShoopingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(sc => sc.Id);

            builder.HasIndex(sc => sc.UserName).IsUnique();

            builder.Property(sc => sc.UserName).IsRequired().HasMaxLength(100);

            builder.HasMany(sc => sc.Items).WithOne()
                .HasForeignKey(sci => sci.ShoppingCartId);
        }
    }
}
