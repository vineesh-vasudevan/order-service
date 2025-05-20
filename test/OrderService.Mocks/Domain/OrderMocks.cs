using OrderService.Domain.Entities;

namespace OrderService.Mocks.Domain
{
    public static class OrderMocks
    {
        public static Order MockOrder(Guid orderId, Guid orderItemId)
        {
            var item = new OrderItemBuilder()
                .WithId(orderItemId)
                .WithOrderId(orderId)
                .WithQuantity(3)
                .WithUnitPrice(20.00m)
                .Build();

            return new OrderBuilder()
               .WithId(orderId)
               .WithItem(item)
               .Build();
        }
    }
}