namespace EshopModulith.Basket.Data.Configurations
{
    internal class ShoopingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(sci => sci.Id);

            builder.Property(sci => sci.ProductId).IsRequired();

            builder.Property(sci => sci.Quantity).IsRequired();

            builder.Property(sci => sci.Color);

            builder.Property(sci => sci.Price).IsRequired();

            builder.Property(sci => sci.ProductName).IsRequired();
        }
    }
}
