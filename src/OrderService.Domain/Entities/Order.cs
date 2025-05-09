
namespace OrderService.Domain.Entities
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => _items.Sum(x => x.TotalPrice);
            private set { }
        }

        public static Order Create(
            OrderId id,
            CustomerId customerId,
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            DateTime createdAt,
            DateTime lastModifiedAt,
            string createdBy,
            string lastModifiedBy)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("OrderId is required.", nameof(id));
            if (customerId.Value == Guid.Empty)
                throw new ArgumentException("CustomerId is required.", nameof(customerId));
            if (string.IsNullOrEmpty(orderName.Value))
                throw new ArgumentNullException(nameof(orderName));
            if (shippingAddress is null)
                throw new ArgumentNullException(nameof(shippingAddress));
            if (billingAddress is null)
                throw new ArgumentNullException(nameof(billingAddress));
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            return new Order
            {
                Id = id,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = OrderStatus.Pending,
                CreatedAt = createdAt,
                LastModifiedAt = lastModifiedAt,
                CreatedBy = createdBy,
                LastModifiedBy = lastModifiedBy,
            };
        }

        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Address billingAddress,
            Payment payment,
            OrderStatus status,
            DateTime createdAt,
            DateTime lastModifiedAt,
            string createdBy,
            string lastModifiedBy)
        {
            if (string.IsNullOrEmpty(orderName.Value))
                throw new ArgumentNullException(nameof(orderName));
            if (shippingAddress is null)
                throw new ArgumentNullException(nameof(shippingAddress));
            if (billingAddress is null)
                throw new ArgumentNullException(nameof(billingAddress));
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

        OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;
            CreatedAt = createdAt;
            LastModifiedAt = lastModifiedAt;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public void Add(OrderItem orderItem)
        {
            if (orderItem is null)
                throw new ArgumentNullException(nameof(orderItem));
            if (orderItem.OrderId != Id)
                throw new InvalidOperationException("OrderItem does not belong to this order.");
            if (_items.Any(x => x.Id == orderItem.Id))
                throw new InvalidOperationException($"OrderItem with ID '{orderItem.Id}' already exists.");

            _items.Add(orderItem);
        }

        public void Remove(OrderItemId id)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("OrderItemId is invalid.", nameof(id));
            var orderItem = _items.FirstOrDefault(x => x.Id == id) ?? throw new OrderItemNotFoundException(id);
            _items.Remove(orderItem);
        }
    }
}
