using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Mocks
{
    public class OrderBuilder
    {
        private OrderId _id = new(Guid.NewGuid());
        private CustomerId _customerId = new(Guid.NewGuid());
        private OrderName _orderName = new("Test Order");
        private Address _shippingAddress = Address.Create("John", "Wick", "123 Main St", "CityA", "StateA", "10001", "US");
        private Address _billingAddress = Address.Create("John", "Wick", "123 Main St", "CityA", "StateA", "10001", "US");
        private Payment _payment = Payment.Create(
            amount: 129.98m,
            currency: Currency.USD,
            paidAt: DateTime.UtcNow,
            paymentMethod: "Credit Card",
            isSuccessful: true,
            transactionId: TransactionId.Of("TXN123456"));

        private List<OrderItem> _items = new();

        public OrderBuilder WithId(Guid id)
        {
            _id = new OrderId(id);
            return this;
        }

        public OrderBuilder WithCustomerId(Guid customerId)
        {
            _customerId = new CustomerId(customerId);
            return this;
        }

        public OrderBuilder WithOrderName(string name)
        {
            _orderName = new OrderName(name);
            return this;
        }

        public OrderBuilder WithShippingAddress(Address address)
        {
            _shippingAddress = address;
            return this;
        }

        public OrderBuilder WithBillingAddress(Address address)
        {
            _billingAddress = address;
            return this;
        }

        public OrderBuilder WithPayment(Payment payment)
        {
            _payment = payment;
            return this;
        }

        public OrderBuilder WithItem(OrderItem item)
        {
            _items.Add(item);
            return this;
        }

        public Order Build()
        {
            var order = Order.Create(_id, _customerId, _orderName, _shippingAddress, _billingAddress, _payment);

            foreach (var item in _items)
            {
                order.Add(item);
            }

            order.SetAudit("System", true);

            return order;
        }
    }
}
