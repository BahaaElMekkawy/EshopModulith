using EshopModulith.Shared.Exceptions;

namespace EshopModulith.Ordering.Orders.Exceptions
{
    public class OrderNotFoundException(Guid Id) : NotFoundException("Order", Id)
    {
    }
}
