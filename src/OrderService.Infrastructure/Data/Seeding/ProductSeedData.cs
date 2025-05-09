
namespace OrderService.Infrastructure.Data.Seeding
{
    internal class ProductSeedData
    {
        public static IEnumerable<Product> GetSeedProducts()
        {
            return
            [
                Product.Create(
                    id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000001")),
                    name: "Dell Mouse",
                    price: 29.99m,
                    description: "Wireless mouse.",
                    code: "PROD001"
                ),
                Product.Create(
                    id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000002")),
                    name: "Dell Keyboard",
                    price: 79.99m,
                    description: "Wireless Keyboard.",
                    code: "PROD002"
                ),
                Product.Create(
                    id: ProductId.Of(Guid.Parse("a1f1c111-0000-4000-a000-000000000003")),
                    name: "Dell USB-C",
                    price: 49.99m,
                    description: "USB-C",
                    code: "PROD003"
                )
            ];
        }
    }
}
