using EshopModulith.Ordering.Orders.Events;
using EshopModulith.Ordering.Orders.ValueObjects;

namespace EshopModulith.Ordering.Orders.Models
{
    public class Order : Aggregate<Guid>
    {
        private readonly List<OrderItem> _items = new();
        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

        public Guid CustomerId { get; private set; } = default;
        public string OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);

        public static Order Create(Guid id, Guid customerId, string orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public void Add(Guid productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {

                existingItem.Quantity += quantity;
            }
            else
            {
                var orderItem = new OrderItem(Id, productId, quantity, price);
                _items.Add(orderItem);
            }
        }

        public void Remove(Guid productId)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                _items.Remove(existingItem);
            }
        }

    }
}
