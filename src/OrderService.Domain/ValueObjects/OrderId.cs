namespace OrderService.Domain.ValueObjects
{
    public readonly record struct OrderId
    {
        public Guid Value { get; }

        public OrderId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("OrderId cannot be empty.");

            Value = value;
        }

        public static OrderId New() => new(Guid.NewGuid());

        public static OrderId Of(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}