using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Mocks
{
    public class OrderItemBuilder
    {
        private OrderItemId _id = new(Guid.NewGuid());
        private OrderId _orderId = new(Guid.NewGuid());
        private string _productCode = "P001";
        private int _quantity = 2;
        private decimal _unitPrice = 10.00m;

        public OrderItemBuilder WithId(Guid id)
        {
            _id = new OrderItemId(id);
            return this;
        }

        public OrderItemBuilder WithOrderId(Guid orderId)
        {
            _orderId = new OrderId(orderId);
            return this;
        }

        public OrderItemBuilder WithProductCode(string code)
        {
            _productCode = code;
            return this;
        }

        public OrderItemBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public OrderItemBuilder WithUnitPrice(decimal price)
        {
            _unitPrice = price;
            return this;
        }

        public OrderItem Build()
        {
            var totalPrice = _unitPrice * _quantity;

            var orderItem = OrderItem.Create(_id, _orderId, _productCode, _quantity, _unitPrice, totalPrice);
            orderItem.SetAudit("System", true);
            return orderItem;
        }
    }
}
