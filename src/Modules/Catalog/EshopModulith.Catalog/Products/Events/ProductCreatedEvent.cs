
namespace EshopModulith.Catalog.Products.Events
{
    public record ProductCreatedEvent(Product Product) : IDomainEvent; //The Product => Primary Constructor

}
// Equivalent longer form
//public record ProductCreatedEvent : IDomainEvent
//{
//    public Product Product { get; init; }

//    public ProductCreatedEvent(Product product)
//    {
//        Product = product;
//    }
//}



