
namespace OrderService.Domain.ValueObjects
{
    public readonly record struct ProductId
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("ProductId cannot be empty.");

            Value = value;
        }

        public static ProductId New() => new(Guid.NewGuid());

        public static ProductId Of(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}
