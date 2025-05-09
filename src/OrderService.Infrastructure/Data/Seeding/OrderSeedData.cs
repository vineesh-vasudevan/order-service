
namespace OrderService.Infrastructure.Data.Seeding
{
    public static class OrderSeedData
    {
        public static IEnumerable<Order> GetSeedOrders()
        {
            var orderId = OrderId.Of(Guid.Parse("33333333-3333-3333-3333-333333333333"));

            var order = Order.Create(
                id: orderId,
                customerId: CustomerSeedData.CustomerOne,
                orderName: new OrderName("First Order"),
                shippingAddress: Address.Create("John", "Wick", "123 Main St", "CityA", "StateA", "10001", "US"),
                billingAddress: Address.Create("John", "Wick", "123 Main St", "CityA", "StateA", "10001", "US"),
                payment: Payment.Create(
                    amount: 129.98m,
                    currency: Currency.USD,
                    paidAt: DateTime.UtcNow,
                    paymentMethod: "Credit Card",
                    isSuccessful: true,
                    transactionId: TransactionId.Of("TXN123456")
                ),
                createdAt: DateTime.UtcNow,
                lastModifiedAt: DateTime.UtcNow,
                createdBy: "seeder",
                lastModifiedBy: "seeder"
            );

            order.Add(OrderItem.Create(
                id: OrderItemId.Of(Guid.Parse("44444444-4444-4444-4444-444444444444")),
                orderId: orderId,
                productCode: "PROD001",
                quantity: 1,
                unitPrice: 49.99m,
                totalPrice: 49.99m,
                createdAt: DateTime.UtcNow,
                lastModifiedAt: DateTime.UtcNow,
                createdBy: "seeder",
                lastModifiedBy: "seeder"
            ));

            order.Add(OrderItem.Create(
                id: OrderItemId.Of(Guid.Parse("55555555-5555-5555-5555-555555555555")),
                orderId: orderId,
                productCode: "PROD002",
                quantity: 2,
                unitPrice: 39.99m,
                totalPrice: 79.98m,
                createdAt: DateTime.UtcNow,
                lastModifiedAt: DateTime.UtcNow,
                createdBy: "seeder",
                lastModifiedBy: "seeder"
            ));

            return [order];
        }
    }
}
