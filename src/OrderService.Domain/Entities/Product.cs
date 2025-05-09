
namespace OrderService.Domain.Entities
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Code { get; private set; } = default!;

        public static Product Create(ProductId id, string name, decimal price, string description, string code)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("ProductId is required.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be non-negative.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Product code is required.", nameof(code));

            return new Product
            {
                Id = id,
                Name = name,
                Price = price,
                Description = description,
                Code = code
            };
        }
    }
}
