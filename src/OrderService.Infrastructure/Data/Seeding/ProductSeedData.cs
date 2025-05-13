namespace OrderService.Infrastructure.Data.Seeding
{
    internal class ProductSeedData
    {
        public static IEnumerable<Product> GetSeedProducts()
        {
            var product1 = Product.Create(
                id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000001")),
                name: "Dell Mouse",
                price: 29.99m,
                description: "Wireless mouse.",
                code: "PROD001"
            );

            product1.SetAudit("System", true);

            var product2 = Product.Create(
                id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000002")),
                name: "Dell Keyboard",
                price: 79.99m,
                description: "Wireless Keyboard.",
                code: "PROD002"
            );

            product2.SetAudit("System", true);

            var product3 = Product.Create(
                id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000003")),
                name: "Dell USB-C",
                price: 49.99m,
                description: "USB-C",
                code: "PROD003"
            );

            product3.SetAudit("System", true);

            return
            [
                product1,
                product2,
                product3
            ];
        }
    }
}