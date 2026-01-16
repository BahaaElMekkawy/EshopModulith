using EshopModulith.Shared.Exceptions;

namespace EshopModulith.Basket.Basket.Exceptions
{
    internal class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string UserName) : base($"Basket With UserName '{UserName}' Was Not Found.")
        { }
    }
}
