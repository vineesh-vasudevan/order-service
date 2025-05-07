
namespace OrderService.Domain.Entities
{
    public class Product: Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Code { get; private set; } = default!;

        private Product(ProductId id, string name, decimal price, string description, string code)
        {
            if (id.Value == Guid.Empty)
                throw new ArgumentException("ProductId is required.", nameof(id));

            Name = string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentException("Product name is required.", nameof(name))
                : name.Trim();

            Price = price < 0
                ? throw new ArgumentOutOfRangeException(nameof(price), "Price must be non-negative.")
                : price;

            Description = description?.Trim() ?? string.Empty;
            Code = string.IsNullOrWhiteSpace(code)
                ? throw new ArgumentException("Product code is required.", nameof(code))
                : code.Trim();

            Id = id;
        }

        public static Product Create(ProductId id, string name, decimal price, string description, string code)
        {
            return new Product(id, name, price, description, code);
        }
    }
}
