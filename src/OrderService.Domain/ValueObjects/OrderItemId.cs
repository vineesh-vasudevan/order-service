namespace OrderService.Domain.ValueObjects
{
    public readonly record struct OrderItemId
    {
        public Guid Value { get; }

        public OrderItemId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("OrderItemId cannot be empty.");

            Value = value;
        }

        public static OrderItemId New() => new(Guid.NewGuid());

        public static OrderItemId Of(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}