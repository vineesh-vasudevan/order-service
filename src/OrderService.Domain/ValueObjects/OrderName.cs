
namespace OrderService.Domain.ValueObjects
{
    public readonly record struct OrderName
    {
        public string Value { get; }

        public OrderName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Order name cannot be null or empty.", nameof(value));

            Value = value.Trim();
        }

        public static OrderName Of(string value) => new(value);

        public override string ToString() => Value;
    }

}
