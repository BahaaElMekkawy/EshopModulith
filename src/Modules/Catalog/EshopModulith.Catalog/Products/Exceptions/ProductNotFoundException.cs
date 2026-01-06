using EshopModulith.Shared.Exceptions;

namespace EshopModulith.Catalog.Products.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product",id)
        {
        }
    }
}
